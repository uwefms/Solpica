namespace AppConst;

public static class Constants
{
	#region Constants per session

	/// <summary>
	/// set 1x in Program.cs valid for all App Instances
	/// </summary>		
	public static string? DataModel { get; set; }

	#endregion Constants per session

	#region enums for CSLA - not used yet

		/// <summary>
		/// Enum für InputMode bei den Eingabecontrols
		/// </summary>
		//public enum InputMode : int
		//{
		//	 input = 1
		//	,textarea = 2
		//	,textareaRO = 3
		//}

		#endregion enums

	#region Constants for UI Project

	// automatic Admin Login - Testing ONLY !!!!
	// public const bool IsFakeLogin = true;
	// public const bool IsFakeLogin = false;

	public const bool IsTesting = true;
	// public const bool IsTesting = false;

	public const bool IsMainMenu = true;
	// public const bool IsMainMenu = false;

	#endregion Constants for UI Project

	#region Constants for Dal Project - not used yet

    // public const string ConnectionName		= "SqlContext";
	// public const string ConnectionSecName	= "Security";
	// public const string ProviderName		= "System.Data.SqlClient";

	// in Permission table
	// public const string AppName				= "Schultraeger";		
	// public const string StartRole			= "StartSchultraeger";
	
	#endregion Constants for Dal Project
	
} // end if class