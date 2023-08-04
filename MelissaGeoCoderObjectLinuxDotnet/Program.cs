using System;
using System.Collections.Generic;
using System.Reflection;
using MelissaData;
using System.Net;

namespace MelissaGeoCoderObjectLinuxDotnet
{
  class Program
  {
    static void Main(string[] args)
    {
      // Variables
      string license = "";
      string dataPath = "";
      string testZip = "";

      ParseArguments(ref license, ref testZip, ref dataPath, args);
      RunAsConsole(license, testZip, dataPath);
    }
     
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

    static void RunAsConsole(string license, string testZip, string dataPath)
    {
      Console.WriteLine("\n\n========== WELCOME TO MELISSA GEOCODER OBJECT LINUX DOTNET =========\n");
     
      GeoObject geoObject = new GeoObject(license, dataPath);

      bool shouldContinueRunning = true;

      if (geoObject.mdGeoObj.GetInitializeErrorString() != "No error")
      {
        shouldContinueRunning = false;
      }

      while (shouldContinueRunning)
      {
        DataContainer dataContainer = new DataContainer();

        if (string.IsNullOrEmpty(testZip))
        {
          Console.WriteLine("\nFill in each value to see the GeoCoder Object results");
          Console.WriteLine($"Zip: ");

          Console.CursorTop -= 1;
          Console.CursorLeft = 7;
          dataContainer.Zip = Console.ReadLine();
        }
        else
        {
          dataContainer.Zip = testZip;
        }

        //Print user input 
        Console.WriteLine("\n============================== INPUTS ==============================\n");
        Console.WriteLine($"\t                     Zip: {dataContainer.Zip}");

        // Execute GeoCoder Object
        geoObject.ExecuteObjectAndResultCodes(ref dataContainer);

        // Print Output
        Console.WriteLine("\n============================== OUTPUT ==============================\n");
        Console.WriteLine("\n\tGeoCoder Object Information:");
        Console.WriteLine($"\t             Place Name: {geoObject.mdGeoObj.GetPlaceName()}");
        Console.WriteLine($"\t                 County: {geoObject.mdGeoObj.GetCountyName()}");
        Console.WriteLine($"\tCounty Subdivision Name: {geoObject.mdGeoObj.GetCountySubdivisionName()}");
        Console.WriteLine($"\t              Time Zone: {geoObject.mdGeoObj.GetTimeZone()}");
        Console.WriteLine($"\t               Latitude: {geoObject.mdGeoObj.GetLatitude()}");
        Console.WriteLine($"\t              Longitude: {geoObject.mdGeoObj.GetLongitude()}");
        Console.WriteLine($"\t           Result Codes: {dataContainer.ResultCodes}");

        String[] rs = dataContainer.ResultCodes.Split(',');
        foreach (String r in rs)
          Console.WriteLine($"        {r}: {geoObject.mdGeoObj.GetResultCodeDescription(r, mdGeo.ResultCdDescOpt.ResultCodeDescriptionLong)}");

        bool isValid = false;
        if (!string.IsNullOrEmpty(testZip))
        {
          isValid = true;
          shouldContinueRunning = false;
        }
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

  class GeoObject
  {
    // Path to GeoCoder Object data files (.dat, etc)
    string dataFilePath;

    // Create instance of Melissa GeoCoder Object
    public mdGeo mdGeoObj = new mdGeo();

    public GeoObject(string license, string dataPath)
    {
      // Set license string and set path to data files (.dat, etc)
      mdGeoObj.SetLicenseString(license);
      dataFilePath = dataPath;

      //Set data paths for objects
      mdGeoObj.SetPathToGeoCodeDataFiles(dataFilePath);
      mdGeoObj.SetPathToGeoCanadaDataFiles(dataFilePath);
      mdGeoObj.SetPathToGeoPointDataFiles(dataFilePath);

      // If you see a different date than expected, check your license string and either download the new data files or use the Melissa Updater program to update your data files.  
      mdGeo.ProgramStatus pStatus = mdGeoObj.InitializeDataFiles();

      if (pStatus != mdGeo.ProgramStatus.ErrorNone)
      {
        Console.WriteLine("Fail to Initialize Object.");
        Console.WriteLine(pStatus);
        return;
      }

      Console.WriteLine($"                DataBase Date: {mdGeoObj.GetDatabaseDate()}");
      Console.WriteLine($"              Expiration Date: {mdGeoObj.GetLicenseExpirationDate()}");

      /**
       * This number should match with file properties of the Melissa Object binary file.
       * If TEST appears with the build number, there may be a license key issue.
       */
      Console.WriteLine($"               Object Version: {mdGeoObj.GetBuildNumber()}\n");
    }

    // This will call the lookup function to process the input zip as well as generate the result codes
    public void ExecuteObjectAndResultCodes(ref DataContainer data)
    {
      mdGeoObj.SetInputParameter("Zip", data.Zip);

      mdGeoObj.FindGeo();
      data.ResultCodes = mdGeoObj.GetResults();

      // ResultsCodes explain any issues GeoCoder Object has with the object.
      // List of result codes for GeoCoder Object
      // https://wiki.melissadata.com/?title=Result_Code_Details#GeoCoder_Object
    }
  }

  public class DataContainer
  {
    public string Zip { get; set; }
    public string ResultCodes { get; set; } = "";
  }
}
