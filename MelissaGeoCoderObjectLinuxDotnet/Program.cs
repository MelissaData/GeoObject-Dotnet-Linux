using System;
using System.Collections.Generic;
using System.Reflection;
using MelissaData;
using System.Net;

namespace MelissaGeoCoderObjectLinuxDotnet
{
  /// <summary>
  /// GeoCoder Object enables you to access geographic data using your ZIP Code and optional Plus4. 
  /// This allows you to obtain latitude and longitude geographic coordinates, census tract and 
  /// block numbers, as well as county name and FIPS numbers.
  /// </summary>
  /// <remarks>
  /// High-level flow of this sample:
  ///   1. SETUP     - create an mdGeo instance, hand it the license string and the
  ///                  paths to the data files, then InitializeDataFiles() (one time).
  ///   2. INPUT     - supply a ZIP via SetInputParameter("Zip", ...).
  ///   3. PROCESS   - FindGeo() looks up the geographic data for that ZIP.
  ///   4. READ      - pull the results back out with the Get* getters
  ///                  (GetLatitude, GetLongitude, GetCountyName, GetPlaceName, ...).
  ///   5. INTERPRET - GetResults() returns comma-separated result codes describing
  ///                  what the object did/found; each code has a human description.
  ///
  /// The pieces in this file map onto that flow:
  ///   - Program        : console harness (argument parsing + the interactive loop).
  ///   - GeoObject      : thin wrapper around mdGeo that owns setup + the call sequence.
  ///   - DataContainer  : plain holder for one record's input and output.
  ///
  /// Where mdGeo comes from:
  ///   The MelissaData namespace and its mdGeo class live in mdGeo_cSharpCode.cs,
  ///   a generated C# wrapper over libmdGeo.so that the accompanying
  ///   MelissaGeoCoderObjectLinuxDotnet.sh script downloads on every run.
  ///
  /// Reference:
  ///   Quickstart    : https://docs.melissa.com/on-premise-api/geocoder-object/geocoder-object-quickstart.html
  ///   Release notes : https://releasenotes.melissa.com/on-premise-api/geocoder-object/
  ///   Result codes  : https://docs.melissa.com/on-premise-api/geocoder-object/result-codes.html
  /// </remarks>
  class Program
  {
    /// <summary>
    /// Entry point. Reads the optional command-line arguments, then hands control to
    /// RunAsConsole, which performs the actual GeoCoder Object setup and processing.
    /// </summary>
    /// <param name="args">The raw command-line arguments</param>
    static void Main(string[] args)
    {
      // Populated by ParseArguments below.
      string license = "";
      string dataPath = "";
      string testZip = "";

      ParseArguments(ref license, ref testZip, ref dataPath, args);
      RunAsConsole(license, testZip, dataPath);
    }
     
    /// <summary>
    /// Reads the supported command-line options into the ref parameters.
    ///
    /// Recognized flags (each followed by its value, e.g. "--zip 92688"):
    ///   --license / -l   : the Melissa license string
    ///   --dataPath / -d  : path to the GeoCoder Object data files
    ///   --zip / -z       : a ZIP code to test in one-shot mode
    /// </summary>
    /// <param name="license">Receives the Melissa license string.</param>
    /// <param name="testZip">Receives the ZIP code to test in one-shot mode.</param>
    /// <param name="dataPath">Receives the path to the GeoCoder Object data files.</param>
    /// <param name="args">The raw command-line arguments to parse.</param>
    static void ParseArguments (ref string license, ref string testZip, ref string dataPath, string[] args)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (args[i].Equals("--license") || args[i].Equals("-l"))
        {
          if (args[i + 1] != null)
          {
            license = args[i + 1];
          }
        }
        if (args[i].Equals("--dataPath") || args[i].Equals("-d"))
        {
          if (args[i + 1] != null)
          {
            dataPath = args[i + 1];
          }
        }
        if (args[i].Equals("--zip") || args[i].Equals("-z"))
        {
          if (args[i + 1] != null)
          {
            testZip = args[i + 1];
          }
        }
      }
    }

    /// <summary>
    /// Sets up the GeoCoder Object once, then drives the input -> process -> output cycle.
    ///
    /// In interactive mode (no --zip) it loops, asking for a new ZIP each pass until the
    /// user answers "N". In one-shot mode (--zip supplied) it runs a single pass on
    /// testZip and exits.
    /// </summary>
    /// <param name="license">The Melissa license string used to initialize the object.</param>
    /// <param name="testZip">A ZIP code to process in one-shot mode; if empty, the program prompts interactively.</param>
    /// <param name="dataPath">Path to the GeoCoder Object data files.</param>
    static void RunAsConsole(string license, string testZip, string dataPath)
    {
      Console.WriteLine("\n\n========== WELCOME TO MELISSA GEOCODER OBJECT LINUX DOTNET =========\n");
     
      // Construct the wrapper. This is where the object is licensed, pointed at the
      // data files, and initialized (see the GeoObject constructor below).
      GeoObject geoObject = new GeoObject(license, dataPath);

      bool shouldContinueRunning = true;

      // Gate the program on a successful initialization. If the data files could not
      // be loaded (bad/expired license, missing or wrong-path data files, ...),
      // GetInitializeErrorString() returns the reason instead of "No error" and we
      // skip the processing loop entirely.
      if (geoObject.mdGeoObj.GetInitializeErrorString() != "No error")
      {
        shouldContinueRunning = false;
      }

      while (shouldContinueRunning)
      {
        // Holder for this pass's input and result codes.
        DataContainer dataContainer = new DataContainer();

        if (string.IsNullOrEmpty(testZip))
        {
          // Interactive mode: prompt the user for a ZIP code.
          Console.WriteLine("\nFill in each value to see the GeoCoder Object results");
          Console.WriteLine($"Zip: ");

          Console.CursorTop -= 1;
          Console.CursorLeft = 7;
          dataContainer.Zip = Console.ReadLine();
        }
        else
        {
          // One-shot mode: use the ZIP passed on the command line.
          dataContainer.Zip = testZip;
        }

        //Print user input 
        Console.WriteLine("\n============================== INPUTS ==============================\n");
        Console.WriteLine($"\t                     Zip: {dataContainer.Zip}");

        // Execute GeoCoder Object
        // Runs the FindGeo lookup and stores the result codes on dataContainer.
        geoObject.ExecuteObjectAndResultCodes(ref dataContainer);

        // Print Output
        // Each Get* getter below returns one component the object produced for the most
        // recently processed ZIP. These read directly from the mdGeo instance, which
        // still holds the results from the Execute call above.
        Console.WriteLine("\n============================== OUTPUT ==============================\n");
        Console.WriteLine("\n\tGeoCoder Object Information:");
        Console.WriteLine($"\t             Place Name: {geoObject.mdGeoObj.GetPlaceName()}");
        Console.WriteLine($"\t                 County: {geoObject.mdGeoObj.GetCountyName()}");
        Console.WriteLine($"\tCounty Subdivision Name: {geoObject.mdGeoObj.GetCountySubdivisionName()}");
        Console.WriteLine($"\t              Time Zone: {geoObject.mdGeoObj.GetTimeZone()}");
        Console.WriteLine($"\t               Latitude: {geoObject.mdGeoObj.GetLatitude()}");
        Console.WriteLine($"\t              Longitude: {geoObject.mdGeoObj.GetLongitude()}");
        Console.WriteLine($"\t           Result Codes: {dataContainer.ResultCodes}");

        // Result codes come back as a single comma-separated string (e.g. "GS05,GS06").
        // Split it and ask the object for a readable description of each code.
        // ResultCodeDescriptionLong requests the long-form text; a short form is also
        // available via ResultCodeDescriptionShort
        String[] rs = dataContainer.ResultCodes.Split(',');
        foreach (String r in rs)
          Console.WriteLine($"        {r}: {geoObject.mdGeoObj.GetResultCodeDescription(r, mdGeo.ResultCdDescOpt.ResultCodeDescriptionLong)}");

        bool isValid = false;

        // In one-shot mode there is nothing more to do after a single pass: mark the
        // input handled and stop the outer loop.
        if (!string.IsNullOrEmpty(testZip))
        {
          isValid = true;
          shouldContinueRunning = false;
        }

        // Interactive mode: ask whether to process another ZIP. Keep prompting until
        // we get a valid Y/N. "N" ends the program; "Y" falls through to another pass.
        while (!isValid)
        {
          Console.WriteLine("\nTest another zip? (Y/N)");
          string testAnotherResponse = Console.ReadLine();

          if (!string.IsNullOrEmpty(testAnotherResponse))
          {
            testAnotherResponse = testAnotherResponse.ToLower();

            if (testAnotherResponse == "y")
            {
              isValid = true;
            }
            else if (testAnotherResponse == "n")
            {
              isValid = true;
              shouldContinueRunning = false;
            }
            else
            {
              Console.Write("Invalid Response, please respond 'Y' or 'N'");
            }
          }
        }
      }
      Console.WriteLine("\n========= THANK YOU FOR USING MELISSA DOTNET OBJECT ========\n");
    }
  }

  /// <summary>
  /// Wrapper that owns a single Melissa GeoCoder Object instance and encapsulates the two
  /// things every Melissa object needs: one-time setup (license + data files) and the
  /// per-record processing sequence. Reuse one instance across many lookups; do NOT
  /// re-initialize per lookup.
  /// </summary>
  class GeoObject
  {
    // Path to the GeoCoder Object data files.
    string dataFilePath;

    // The underlying Melissa GeoCoder Object instance.
    public mdGeo mdGeoObj = new mdGeo();

    /// <summary>
    /// Performs the mandatory one-time setup, in this required order:
    ///   1. SetLicenseString     - authorize the object.
    ///   2. SetPathTo*DataFiles  - tell it where each set of data files lives.
    ///   3. InitializeDataFiles  - load the data into memory.
    /// </summary>
    /// <param name="license">The Melissa license string used to authorize the object.</param>
    /// <param name="dataPath">Path to the folder containing the GeoCoder Object data files.</param>
    public GeoObject(string license, string dataPath)
    {
      // Set license string and set path to data files
      mdGeoObj.SetLicenseString(license);
      dataFilePath = dataPath;

      // Point the object at the GeoCoder data files (US GeoCode, Canada, and GeoPoint).
      mdGeoObj.SetPathToGeoCodeDataFiles(dataFilePath);
      mdGeoObj.SetPathToGeoCanadaDataFiles(dataFilePath);
      mdGeoObj.SetPathToGeoPointDataFiles(dataFilePath);

      // Load the data files. The returned ProgramStatus reports whether initialization succeeded.
      // If you see a different date than expected, check your license string and either download the new data files
      // or use the Melissa Updater program to update your data files.
      mdGeo.ProgramStatus pStatus = mdGeoObj.InitializeDataFiles();

      // If an issue occurred, please investigate the common causes.
      // Common causes: an invalid/expired license, or missing/wrong-path data files.
      if (pStatus != mdGeo.ProgramStatus.ErrorNone)
      {
        Console.WriteLine("Fail to Initialize Object.");
        Console.WriteLine(pStatus);
        return;
      }

      // Diagnostic information, handy for confirming the object loaded the data you expect:

      // Build date of the data files
      Console.WriteLine($"                DataBase Date: {mdGeoObj.GetDatabaseDate()}");

      // When the license stops working
      Console.WriteLine($"              Expiration Date: {mdGeoObj.GetLicenseExpirationDate()}");

      // This number should match with the file properties of the Melissa Object binary file.
      // If TEST appears with the build number, there may be a license key issue.
      Console.WriteLine($"               Object Version: {mdGeoObj.GetBuildNumber()}\n");
    }

    /// <summary>
    /// Runs the full GeoCoder Object processing sequence for one ZIP and captures its
    /// result codes. This is the canonical per-record call pattern to copy into your
    /// own application:
    ///   SetInputParameter -> FindGeo -> GetResults
    /// </summary>
    /// <param name="data">
    /// The record to process. Its Zip is read as input, and ResultCodes is populated
    /// with this run's result codes.
    /// </param>
    public void ExecuteObjectAndResultCodes(ref DataContainer data)
    {

      // Supply the ZIP to look up
      mdGeoObj.SetInputParameter("Zip", data.Zip);

      // Look up the geographic data for that ZIP
      mdGeoObj.FindGeo();
      // Collect the result codes for this run
      // ResultsCodes explain any issues GeoCoder Object has with the object.
      // List of result codes for GeoCoder Object
      // https://docs.melissa.com/on-premise-api/geocoder-object/result-codes.html
      data.ResultCodes = mdGeoObj.GetResults();
    }
  }

  /// <summary>
  /// Data holder for a single record: carries the input ZIP in and the result codes out.
  /// </summary>
  public class DataContainer
  {
    // Input: the ZIP code to look up.
    public string Zip { get; set; }

    // Output: comma-separated result codes from GetResults().
    public string ResultCodes { get; set; } = "";
  }
}
