using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Security;

namespace MelissaData {
	public class mdGeo : IDisposable {
		private IntPtr i;

		public enum ProgramStatus {
			ErrorNone = 0,
			ErrorOther = 1,
			ErrorOutOfMemory = 2,
			ErrorRequiredFileNotFound = 3,
			ErrorFoundOldFile = 4,
			ErrorDatabaseExpired = 5,
			ErrorLicenseExpired = 6
		}
		public enum AccessType {
			Local = 0,
			Remote = 1
		}
		public enum DiacriticsMode {
			Auto = 0,
			On = 1,
			Off = 2
		}
		public enum StandardizeMode {
			ShortFormat = 0,
			LongFormat = 1,
			AutoFormat = 2
		}
		public enum SuiteParseMode {
			ParseSuite = 0,
			CombineSuite = 1
		}
		public enum AliasPreserveMode {
			ConvertAlias = 0,
			PreserveAlias = 1
		}
		public enum AutoCompletionMode {
			AutoCompleteSingleSuite = 0,
			AutoCompleteRangedSuite = 1,
			AutoCompletePlaceHolderSuite = 2,
			AutoCompleteNoSuite = 3
		}
		public enum ResultCdDescOpt {
			ResultCodeDescriptionLong = 0,
			ResultCodeDescriptionShort = 1
		}
		public enum MailboxLookupMode {
			MailboxNone = 0,
			MailboxExpress = 1,
			MailboxPremium = 2
		}

		[SuppressUnmanagedCodeSecurity]
		private class mdGeoUnmanaged {
			[DllImport("mdGeo", EntryPoint = "mdGeoCreate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoCreate();
			[DllImport("mdGeo", EntryPoint = "mdGeoDestroy", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdGeoDestroy(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoSetPathToGeoCodeDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdGeoSetPathToGeoCodeDataFiles(IntPtr i, IntPtr p1);
			[DllImport("mdGeo", EntryPoint = "mdGeoSetPathToGeoPointDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdGeoSetPathToGeoPointDataFiles(IntPtr i, IntPtr p1);
			[DllImport("mdGeo", EntryPoint = "mdGeoSetPathToGeoCanadaDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdGeoSetPathToGeoCanadaDataFiles(IntPtr i, IntPtr p1);
			[DllImport("mdGeo", EntryPoint = "mdGeoSetLicenseString", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdGeoSetLicenseString(IntPtr i, IntPtr License);
			[DllImport("mdGeo", EntryPoint = "mdGeoInitialize", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdGeoInitialize(IntPtr i, IntPtr DataPath, IntPtr IndexPath);
			[DllImport("mdGeo", EntryPoint = "mdGeoInitializeDataFiles", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdGeoInitializeDataFiles(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetInitializeErrorString", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetInitializeErrorString(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetBuildNumber", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetBuildNumber(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetDatabaseDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetDatabaseDate(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetExpirationDate(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetLicenseExpirationDate", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetLicenseExpirationDate(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoSetLatitude", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdGeoSetLatitude(IntPtr i, IntPtr latitude);
			[DllImport("mdGeo", EntryPoint = "mdGeoSetLongitude", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdGeoSetLongitude(IntPtr i, IntPtr longitude);
			[DllImport("mdGeo", EntryPoint = "mdGeoWriteToLogFile", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdGeoWriteToLogFile(IntPtr i, IntPtr logFile);
			[DllImport("mdGeo", EntryPoint = "mdGeoGeoCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdGeoGeoCode(IntPtr i, IntPtr Zip, IntPtr Plus4);
			[DllImport("mdGeo", EntryPoint = "mdGeoGeoPoint", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdGeoGeoPoint(IntPtr i, IntPtr Zip, IntPtr Plus4, IntPtr DeliveryPointCode);
			[DllImport("mdGeo", EntryPoint = "mdGeoComputeDistance", CallingConvention = CallingConvention.Cdecl)]
			public static extern double mdGeoComputeDistance(IntPtr i, double Latitude1, double Longitude1, double Latitude2, double Longitude2);
			[DllImport("mdGeo", EntryPoint = "mdGeoComputeBearing", CallingConvention = CallingConvention.Cdecl)]
			public static extern double mdGeoComputeBearing(IntPtr i, double Latitude1, double Longitude1, double Latitude2, double Longitude2);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetErrorCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetErrorCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetStatusCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetStatusCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetResults", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetResults(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetResultCodeDescription", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetResultCodeDescription(IntPtr i, IntPtr resultCode, Int32 opt);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetLatitude", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetLatitude(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetLongitude", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetLongitude(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCensusTract", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCensusTract(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCensusBlock", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCensusBlock(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCountyFips", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCountyFips(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCountyName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCountyName(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetPlaceCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetPlaceCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetPlaceName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetPlaceName(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetTimeZoneCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetTimeZoneCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetTimeZone", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetTimeZone(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCBSACode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCBSACode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCBSATitle", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCBSATitle(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCBSALevel", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCBSALevel(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCBSADivisionCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCBSADivisionCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCBSADivisionTitle", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCBSADivisionTitle(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCBSADivisionLevel", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCBSADivisionLevel(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetLastUsageLogMessage", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetLastUsageLogMessage(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCensusKey", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCensusKey(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCountySubdivisionCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCountySubdivisionCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetCountySubdivisionName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetCountySubdivisionName(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetElementarySchoolDistrictCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetElementarySchoolDistrictCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetElementarySchoolDistrictName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetElementarySchoolDistrictName(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetSecondarySchoolDistrictCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetSecondarySchoolDistrictCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetSecondarySchoolDistrictName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetSecondarySchoolDistrictName(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetStateDistrictLower", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetStateDistrictLower(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetStateDistrictUpper", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetStateDistrictUpper(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetUnifiedSchoolDistrictCode", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetUnifiedSchoolDistrictCode(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetUnifiedSchoolDistrictName", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetUnifiedSchoolDistrictName(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetBlockSuffix", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetBlockSuffix(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoSetInputParameter", CallingConvention = CallingConvention.Cdecl)]
			public static extern Int32 mdGeoSetInputParameter(IntPtr i, IntPtr key, IntPtr val);
			[DllImport("mdGeo", EntryPoint = "mdGeoFindGeo", CallingConvention = CallingConvention.Cdecl)]
			public static extern void mdGeoFindGeo(IntPtr i);
			[DllImport("mdGeo", EntryPoint = "mdGeoGetOutputParameter", CallingConvention = CallingConvention.Cdecl)]
			public static extern IntPtr mdGeoGetOutputParameter(IntPtr i, IntPtr key);
		}

		public mdGeo() {
			i = mdGeoUnmanaged.mdGeoCreate();
		}

		~mdGeo() {
			Dispose();
		}

		public virtual void Dispose() {
			lock (this) {
				mdGeoUnmanaged.mdGeoDestroy(i);
				GC.SuppressFinalize(this);
			}
		}

		public void SetPathToGeoCodeDataFiles(string p1) {
			Utf8String u_p1 = new Utf8String(p1);
			mdGeoUnmanaged.mdGeoSetPathToGeoCodeDataFiles(i, u_p1.GetUtf8Ptr());
		}

		public void SetPathToGeoPointDataFiles(string p1) {
			Utf8String u_p1 = new Utf8String(p1);
			mdGeoUnmanaged.mdGeoSetPathToGeoPointDataFiles(i, u_p1.GetUtf8Ptr());
		}

		public void SetPathToGeoCanadaDataFiles(string p1) {
			Utf8String u_p1 = new Utf8String(p1);
			mdGeoUnmanaged.mdGeoSetPathToGeoCanadaDataFiles(i, u_p1.GetUtf8Ptr());
		}

		public bool SetLicenseString(string License) {
			Utf8String u_License = new Utf8String(License);
			return (mdGeoUnmanaged.mdGeoSetLicenseString(i, u_License.GetUtf8Ptr()) != 0);
		}

		public ProgramStatus Initialize(string DataPath, string IndexPath) {
			Utf8String u_DataPath = new Utf8String(DataPath);
			Utf8String u_IndexPath = new Utf8String(IndexPath);
			return (ProgramStatus)mdGeoUnmanaged.mdGeoInitialize(i, u_DataPath.GetUtf8Ptr(), u_IndexPath.GetUtf8Ptr());
		}

		public ProgramStatus InitializeDataFiles() {
			return (ProgramStatus)mdGeoUnmanaged.mdGeoInitializeDataFiles(i);
		}

		public string GetInitializeErrorString() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetInitializeErrorString(i));
		}

		public string GetBuildNumber() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetBuildNumber(i));
		}

		public string GetDatabaseDate() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetDatabaseDate(i));
		}

		public string GetExpirationDate() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetExpirationDate(i));
		}

		public string GetLicenseExpirationDate() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetLicenseExpirationDate(i));
		}

		public void SetLatitude(string latitude) {
			Utf8String u_latitude = new Utf8String(latitude);
			mdGeoUnmanaged.mdGeoSetLatitude(i, u_latitude.GetUtf8Ptr());
		}

		public void SetLongitude(string longitude) {
			Utf8String u_longitude = new Utf8String(longitude);
			mdGeoUnmanaged.mdGeoSetLongitude(i, u_longitude.GetUtf8Ptr());
		}

		public bool WriteToLogFile(string logFile) {
			Utf8String u_logFile = new Utf8String(logFile);
			return (mdGeoUnmanaged.mdGeoWriteToLogFile(i, u_logFile.GetUtf8Ptr()) != 0);
		}

		public int GeoCode(string Zip, string Plus4) {
			Utf8String u_Zip = new Utf8String(Zip);
			Utf8String u_Plus4 = new Utf8String(Plus4);
			return mdGeoUnmanaged.mdGeoGeoCode(i, u_Zip.GetUtf8Ptr(), u_Plus4.GetUtf8Ptr());
		}

		public int GeoCode(string Zip) {
			Utf8String u_Zip = new Utf8String(Zip);
			Utf8String u_Plus4 = new Utf8String("");
			return mdGeoUnmanaged.mdGeoGeoCode(i, u_Zip.GetUtf8Ptr(), u_Plus4.GetUtf8Ptr());
		}

		public int GeoPoint(string Zip, string Plus4, string DeliveryPointCode) {
			Utf8String u_Zip = new Utf8String(Zip);
			Utf8String u_Plus4 = new Utf8String(Plus4);
			Utf8String u_DeliveryPointCode = new Utf8String(DeliveryPointCode);
			return mdGeoUnmanaged.mdGeoGeoPoint(i, u_Zip.GetUtf8Ptr(), u_Plus4.GetUtf8Ptr(), u_DeliveryPointCode.GetUtf8Ptr());
		}

		public double ComputeDistance(double Latitude1, double Longitude1, double Latitude2, double Longitude2) {
			return mdGeoUnmanaged.mdGeoComputeDistance(i, Latitude1, Longitude1, Latitude2, Longitude2);
		}

		public double ComputeBearing(double Latitude1, double Longitude1, double Latitude2, double Longitude2) {
			return mdGeoUnmanaged.mdGeoComputeBearing(i, Latitude1, Longitude1, Latitude2, Longitude2);
		}

		public string GetErrorCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetErrorCode(i));
		}

		public string GetStatusCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetStatusCode(i));
		}

		public string GetResults() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetResults(i));
		}

		public string GetResultCodeDescription(string resultCode, ResultCdDescOpt opt) {
			Utf8String u_resultCode = new Utf8String(resultCode);
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetResultCodeDescription(i, u_resultCode.GetUtf8Ptr(), (int)opt));
		}

		public string GetResultCodeDescription(string resultCode) {
			Utf8String u_resultCode = new Utf8String(resultCode);
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetResultCodeDescription(i, u_resultCode.GetUtf8Ptr(), (int)ResultCdDescOpt.ResultCodeDescriptionLong));
		}

		public string GetLatitude() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetLatitude(i));
		}

		public string GetLongitude() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetLongitude(i));
		}

		public string GetCensusTract() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCensusTract(i));
		}

		public string GetCensusBlock() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCensusBlock(i));
		}

		public string GetCountyFips() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCountyFips(i));
		}

		public string GetCountyName() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCountyName(i));
		}

		public string GetPlaceCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetPlaceCode(i));
		}

		public string GetPlaceName() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetPlaceName(i));
		}

		public string GetTimeZoneCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetTimeZoneCode(i));
		}

		public string GetTimeZone() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetTimeZone(i));
		}

		public string GetCBSACode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCBSACode(i));
		}

		public string GetCBSATitle() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCBSATitle(i));
		}

		public string GetCBSALevel() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCBSALevel(i));
		}

		public string GetCBSADivisionCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCBSADivisionCode(i));
		}

		public string GetCBSADivisionTitle() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCBSADivisionTitle(i));
		}

		public string GetCBSADivisionLevel() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCBSADivisionLevel(i));
		}

		public string GetLastUsageLogMessage() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetLastUsageLogMessage(i));
		}

		public string GetCensusKey() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCensusKey(i));
		}

		public string GetCountySubdivisionCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCountySubdivisionCode(i));
		}

		public string GetCountySubdivisionName() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetCountySubdivisionName(i));
		}

		public string GetElementarySchoolDistrictCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetElementarySchoolDistrictCode(i));
		}

		public string GetElementarySchoolDistrictName() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetElementarySchoolDistrictName(i));
		}

		public string GetSecondarySchoolDistrictCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetSecondarySchoolDistrictCode(i));
		}

		public string GetSecondarySchoolDistrictName() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetSecondarySchoolDistrictName(i));
		}

		public string GetStateDistrictLower() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetStateDistrictLower(i));
		}

		public string GetStateDistrictUpper() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetStateDistrictUpper(i));
		}

		public string GetUnifiedSchoolDistrictCode() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetUnifiedSchoolDistrictCode(i));
		}

		public string GetUnifiedSchoolDistrictName() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetUnifiedSchoolDistrictName(i));
		}

		public string GetBlockSuffix() {
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetBlockSuffix(i));
		}

		public bool SetInputParameter(string key, string val) {
			Utf8String u_key = new Utf8String(key);
			Utf8String u_val = new Utf8String(val);
			return (mdGeoUnmanaged.mdGeoSetInputParameter(i, u_key.GetUtf8Ptr(), u_val.GetUtf8Ptr()) != 0);
		}

		public void FindGeo() {
			mdGeoUnmanaged.mdGeoFindGeo(i);
		}

		public string GetOutputParameter(string key) {
			Utf8String u_key = new Utf8String(key);
			return Utf8String.GetUnicodeString(mdGeoUnmanaged.mdGeoGetOutputParameter(i, u_key.GetUtf8Ptr()));
		}

		private class Utf8String : IDisposable {
			private IntPtr utf8String = IntPtr.Zero;

			public Utf8String(string str) {
				if (str == null)
					str = "";
				byte[] buffer = Encoding.UTF8.GetBytes(str);
				Array.Resize(ref buffer, buffer.Length + 1);
				buffer[buffer.Length - 1] = 0;
				utf8String = Marshal.AllocHGlobal(buffer.Length);
				Marshal.Copy(buffer, 0, utf8String, buffer.Length);
			}

			~Utf8String() {
				Dispose();
			}

			public virtual void Dispose() {
				lock (this) {
					Marshal.FreeHGlobal(utf8String);
					GC.SuppressFinalize(this);
				}
			}

			public IntPtr GetUtf8Ptr() {
				return utf8String;
			}

			public static string GetUnicodeString(IntPtr ptr) {
				if (ptr == IntPtr.Zero)
					return "";
				int len = 0;
				while (Marshal.ReadByte(ptr, len) != 0)
					len++;
				if (len == 0)
					return "";
				byte[] buffer = new byte[len];
				Marshal.Copy(ptr, buffer, 0, len);
				return Encoding.UTF8.GetString(buffer);
			}
		}
	}
}
