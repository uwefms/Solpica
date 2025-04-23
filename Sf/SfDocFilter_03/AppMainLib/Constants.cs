namespace AppMainLib
{
	/// <summary>
	/// Summary description for Constants.
	/// </summary>
	public class Const
	{		
		/// <summary>
		/// Constructor
		/// </summary>
		private Const()	{}

        public const string xROW_ID                    = "Satz_ID" ;		

        #region SQL Vergleichsoperatoren für Filterfenster

		public const string xFLD_TYPE_INT          =  "int"      ;  // int Feld Type
		public const string xFLD_TYPE_STR          =  "str"      ;  // string Feld Type
		public const string xFLD_TYPE_DTE          =  "date"     ;  // date Feld Type
		public const string xFLD_TYPE_BOOL         =  "bool"     ;  // bool Feld Type
		public const string xFLD_TYPE_DEC          =  "dec"      ;  // Decimal Feld Type


		/// ////////////////////////////////////////////////////////////////////////////////////////
		// Stringvergleiche
		/// ////////////////////////////////////////////////////////////////////////////////////////

		public const string opSTR_GLEICH           =  "=       |'|0|0|'" ;  // <Feld> = '<Var>'
		public const string opSTR_UNGLEICH         =  "NOT LIKE|'|0|%|'" ;  // <Feld> NOT LIKE '<Var%>'

		public const string opSTR_BEGINNT          =  "    LIKE|'|0|%|'" ;  // <Feld> LIKE '<Var%>'
		public const string opSTR_ENDET            =  "    LIKE|'|%|0|'" ;  // <Feld> LIKE '<Var%>'
		public const string opSTR_ENTHAELT         =  "    LIKE|'|%|%|'" ;  // <Feld> LIKE '<%Var%>'

		public const string opSTR_ENTHAELT_NICHT   =  "NOT LIKE|'|%|%|'" ;  // <Feld> LIKE '<%Var%>'

		public const string opSTR_GROESSER         =  "    >   |'|0|0|'" ;  // <Feld> > '<Var>'		
		public const string opSTR_KLEINER          =  "    <   |'|0|0|'" ;  // <Feld> < '<Var>'

		public const string opSTR_GROESSER_GL      =  "    >=  |'|0|0|'" ;  // <Feld> >= '<Var>'
		public const string opSTR_KLEINER_GL       =  "    <=  |'|0|0|'" ;  // <Feld> <= '<Var>'
		
		public const string opSTR_NULL             = " IS NULL|0|0|0|0"       ;  // <Feld> < '<Var>'
		public const string opSTR_NOT_NULL         = " IS NOT NULL|0|0|0|0"   ;  // <Feld> < '<Var>'
				
		/// ////////////////////////////////////////////////////////////////////////////////////////


		public const string opSTR_GLEICH_TXT			= "gleich";  // <Feld> = '<Var>'
		public const string opSTR_UNGLEICH_TXT			= "ungleich";  // <Feld> NOT LIKE '<Var%>'

		public const string opSTR_BEGINNT_TXT			= "beginnt mit";  // <Feld> LIKE '<Var%>'
		public const string opSTR_ENDET_TXT				= "endet mit";  // <Feld> LIKE '<Var%>'
		public const string opSTR_ENTHAELT_TXT			= "enthält";  // <Feld> LIKE '<%Var%>'

		public const string opSTR_ENTHAELT_NICHT_TXT    = "enthält nicht";  // <Feld> LIKE '<%Var%>'

		public const string opSTR_GROESSER_TXT			= "größer";  // <Feld> > '<Var>'		
		public const string opSTR_KLEINER_TXT			= "kleiner";  // <Feld> < '<Var>'

		public const string opSTR_GROESSER_GL_TXT		= "größer oder gleich";  // <Feld> >= '<Var>'
		public const string opSTR_KLEINER_GL_TXT		= "kleiner oder gleich";  // <Feld> <= '<Var>'

		public const string opSTR_NULL_TXT				= "Eintrag ist NULL"       ; // <Feld> < '<Var>'
		public const string opSTR_NOT_NULL_TXT			= "Eintrag ist nicht NULL" ; // <Feld> < '<Var>'

		/// ////////////////////////////////////////////////////////////////////////////////////////
		// Zahlenvergleiche
		/// ////////////////////////////////////////////////////////////////////////////////////////
	   
		public const string opINT_GLEICH           =  "    =   |0|0|0|0" ;  // <Feld> = <Var>
		public const string opINT_UNGLEICH         =  "   !=   |0|0|0|0" ;  // <Feld> != <Var>

		public const string opINT_GROESSER         =  "    >   |0|0|0|0" ;  // <Feld> > <Var>		
		public const string opINT_KLEINER          =  "    <   |0|0|0|0" ;  // <Feld> < <Var>

		public const string opINT_GROESSER_GL      = "    >=   |0|0|0|0" ;  // <Feld> >= <Var>
		public const string opINT_KLEINER_GL       = "    <=   |0|0|0|0" ;  // <Feld> <= <Var>

		public const string opINT_NULL             = " IS NULL|0|0|0|0"       ;  // <Feld> < '<Var>'
		public const string opINT_NOT_NULL         = " IS NOT NULL|0|0|0|0"   ;  // <Feld> < '<Var>'

		/// ////////////////////////////////////////////////////////////////////////////////////////


		public const string opINT_GLEICH_TXT       = "gleich";  // <Feld> = <Var>
		public const string opINT_UNGLEICH_TXT     = "ungleich";  // <Feld> != <Var>

		public const string opINT_GROESSER_TXT     = "größer";  // <Feld> > <Var>		
		public const string opINT_KLEINER_TXT      = "kleiner";  // <Feld> < <Var>

		public const string opINT_GROESSER_GL_TXT  = "größer oder gleich";  // <Feld> >= <Var>
		public const string opINT_KLEINER_GL_TXT   = "kleiner oder gleich";  // <Feld> <= <Var>

		public const string opINT_NULL_TXT         = "Eintrag ist NULL"       ; // <Feld> < '<Var>'
		public const string opINT_NOT_NULL_TXT     = "Eintrag ist nicht NULL" ; // <Feld> < '<Var>'

		/// ////////////////////////////////////////////////////////////////////////////////////////
		// Datumsvergleiche
		/// ////////////////////////////////////////////////////////////////////////////////////////
		
		public const string opDTE_GLEICH           = "    =   |'|0|0|'"    ;  // <Feld> = '<Var>'
		public const string opDTE_UNGLEICH         = "   !=   |'|0|0|'"    ;  // <Feld> != '<Var>'

		public const string opDTE_GROESSER         = "    >   |'|0|0|'"    ;  // <Feld> > '<Var>'		
		public const string opDTE_KLEINER          = "    <   |'|0|0|'"    ;  // <Feld> < '<Var>'
		public const string opDTE_NULL             = " IS NULL|0|0|0|0"    ;  // <Feld> < '<Var>'
		public const string opDTE_NOT_NULL         = " IS NOT NULL|0|0|0|0";  // <Feld> < '<Var>'
		
		public const string opDTE_GLEICH_TXT       = "gleiches Datum"       ;  // <Feld> = '<Var>'
		public const string opDTE_UNGLEICH_TXT     = "ungleiches Datum"     ;  // <Feld> != '<Var>'

		public const string opDTE_GROESSER_TXT     = "nach Vergleichsdatum" ; // <Feld> > '<Var>'		
		public const string opDTE_KLEINER_TXT      = "vor Vergleichsdatum"  ; // <Feld> < '<Var>'
		public const string opDTE_NULL_TXT         = "leeres Datum"         ; // <Feld> < '<Var>'
		public const string opDTE_NOT_NULL_TXT     = "vorhandenes Datum"    ; // <Feld> < '<Var>'

		/// ////////////////////////////////////////////////////////////////////////////////////////

		public const string opLOGIC_TRUE           = "    =   |'|0|Y|'"    ;  // <Feld> = '<Var>'
		public const string opLOGIC_FALSE          = "    =   |'|0|N|'"    ;  // <Feld> != '<Var>'

		public const string opBOOL_TRUE            = "    true   |0|0|0|0" ;  // <Feld> = '<Var>'
		public const string opBOOL_FALSE           = "    false  |0|0|0|0" ;  // <Feld> != '<Var>'
		
		public const string opLOGIC_UNDETREMINED   = " IS NULL|0|0|0|0"    ;  // <Feld> < '<Var>'
		public const string opLOGIC_TRUE_OR_FALSE  = " IS NOT NULL|0|0|0|0";  // <Feld> < '<Var>'

		public const string opLOGIC_TRUE_TXT          = "wahr"             ;  // <Feld> = '<Var>'
		public const string opLOGIC_FALSE_TXT         = "falsch"           ;  // <Feld> != '<Var>'
		public const string opLOGIC_UNDETEREMINED_TXT = "Eintrag ist NULL"		 ;  // <Feld> < '<Var>'
		public const string opLOGIC_TRUE_OR_FALSE_TXT = "Eintrag ist nicht NULL" ;  // <Feld> < '<Var>'

		#endregion SQL Vergleichsoperatoren 

        #region auskommentiert

		// public const string xCOMBO_NULL_TXT  = "< unbekannt >" ;

		/// <summary>
		/// NULL Entsprechung bei numerischen Werten
		/// </summary>
		// public const int xComboNullId	   = -1; 
		
		//comment by VS
		/// <summary>
		/// NULL Entsprechung bei Combobox anzeige
		/// </summary>
		
		// public const string xComboNullText	= "< unbekannt >";

		// public const string CRLF             = "\r\n";

		// public const string xCOMBO_NULL_TXT  = "< unbekannt >" ;        
		        
		#region auskommentiert - Imagelist Konstanten
	
		//public const int xIL_Add_Appointment			=    0  ;
		//public const int xIL_Add_Database_Record		=    1  ;
		//public const int xIL_Alert_or_Warning_1			=    2  ;
		//public const int xIL_Alert_or_Warning_2			=    3  ;
		//public const int xIL_Appointment				=    4  ;
		//public const int xIL_Back						=    5  ;
		//public const int xIL_Bank						=    6  ;
		//public const int xIL_Book_Blue					=    7  ;
		//public const int xIL_Book_Green					=    8  ;
		//public const int xIL_Book_Red					=    9  ;

		//public const int xIL_Books						=   10  ;
		//public const int xIL_Bottom						=   11  ;
		//public const int xIL_Browser_Stop				=   12  ;
		//public const int xIL_Calculator					=   13  ;
		//public const int xIL_Calendar_1_Blank			=   14  ;
		//public const int xIL_Calendar_2_Blank			=   15  ;
		//public const int xIL_Calendar_Edit				=   16  ;
		//public const int xIL_Cash_Register				=   17  ;
		//public const int xIL_Charts_and_Graphs			=   18  ;
		//public const int xIL_Checkbox_Checked			=   19  ;

		//public const int xIL_Checkbox_Crossed			=   20  ;
		//public const int xIL_Clock						=   21  ;
		//public const int xIL_Close_X_Green				=   22  ;
		//public const int xIL_Close_X_Red				=   23  ;
		//public const int xIL_Cog_1						=   24  ;
		//public const int xIL_Cog_2						=   25  ;
		//public const int xIL_Coins						=   26  ;
		//public const int xIL_Contact					=   27  ;
		//public const int xIL_Database					=   28  ;
		//public const int xIL_Database_Add				=   29  ;

		//public const int xIL_Date_and_Time				=   30  ;
		//public const int xIL_Delete_Database_Record		=   31  ;
		//public const int xIL_Delete_Red					=   32  ;
		//public const int xIL_Disconnect_Network			=   33  ;
		//public const int xIL_Dollar						=   34  ;
		//public const int xIL_Down						=   35  ;
		//public const int xIL_Download_from_Web			=   36  ;
		//public const int xIL_Edit						=   37  ;
		//public const int xIL_Edit_Record				=   38  ;
		//public const int xIL_Error_Round_Red			=   39  ;

		//public const int xIL_Euro						=   40  ;
		//public const int xIL_Exclamation_Red			=   41  ;
		//public const int xIL_Exit_Door_1				=   42  ;
		//public const int xIL_Exit_Door_2				=   43  ;
		//public const int xIL_Export_from_Database		=   44  ;
		//public const int xIL_Fast_Forward				=   45  ;
		//public const int xIL_Fast_Rewind				=   46  ;
		//public const int xIL_Favorites_Blue				=   47  ;
		//public const int xIL_Favorites_Yellow			=   48  ;
		//public const int xIL_Filter						=   49  ;

		//public const int xIL_First						=   50  ;
		//public const int xIL_Flag_Black					=   51  ;
		//public const int xIL_Flag_Blue					=   52  ;
		//public const int xIL_Flag_Green					=   53  ;
		//public const int xIL_Flag_Orange				=   54  ;
		//public const int xIL_Flag_Purple				=   55  ;
		//public const int xIL_Flag_Red					=   56  ;
		//public const int xIL_Flag_White					=   57  ;
		//public const int xIL_Flag_Yellow				=   58  ;
		//public const int xIL_Forward					=   59  ;

		//public const int xIL_Forward_or_Next			=   60  ;
		//public const int xIL_Gavel						=   61  ;
		//public const int xIL_Green_Checkmark			=   62  ;
		//public const int xIL_Green_Plus					=   63  ;
		//public const int xIL_Hammer						=   64  ;
		//public const int xIL_Help_Blue					=   65  ;
		//public const int xIL_Help_Bubble				=   66  ;
		//public const int xIL_Help_Green					=   67  ;
		//public const int xIL_Help_Lifesaver				=   68  ;
		//public const int xIL_History					=   69  ;

		//public const int xIL_Import_Doc					=   70  ;
		//public const int xIL_Import_to_Database			=   71  ;
		//public const int xIL_Info_Bubble				=   72  ;
		//public const int xIL_Info_Round_Blue			=   73  ;
		//public const int xIL_Key						=   74  ;
		//public const int xIL_Last						=   75  ;
		//public const int xIL_Light_Black_Round			=   76  ;
		//public const int xIL_Light_Blue_Round			=   77  ;
		//public const int xIL_Light_Bulb_Off				=   78  ;
		//public const int xIL_Light_Bulb_On				=   79  ;

		//public const int xIL_Light_Green_Round			=   80  ;
		//public const int xIL_Light_Grey_Round			=   81  ;
		//public const int xIL_Light_Orange_Round			=   82  ;
		//public const int xIL_Light_Pink_Round			=   83  ;
		//public const int xIL_Light_Purple_Round			=   84  ;
		//public const int xIL_Light_Red_Round			=   85  ;
		//public const int xIL_Light_White_Round			=   86  ;
		//public const int xIL_Light_Yellow_Round			=   87  ;
		//public const int xIL_Line_Graph_with_Markers	=   88  ;
		//public const int xIL_Locked						=   89  ;
		
		//public const int xIL_Money						=   90  ;
		//public const int xIL_Network_Drive				=   91  ;
		//public const int xIL_OK							=   92  ;
		//public const int xIL_Options_1					=   93  ;
		//public const int xIL_Options_2					=   94  ;
		//public const int xIL_Options_3					=   95  ;
		//public const int xIL_Pin_Black					=   96  ;
		//public const int xIL_Pin_Blue					=   97  ;
		//public const int xIL_Pin_Green					=   98  ;
		//public const int xIL_Pin_Orange					=   99  ;

		//public const int xIL_Pin_Purple					=  100  ;
		//public const int xIL_Pin_Red					=  101  ;
		//public const int xIL_Pin_White					=  102  ;
		//public const int xIL_Pin_Yellow					=  103  ;
		//public const int xIL_Plugin						=  104  ;
		//public const int xIL_Print_Preview				=  105  ;
		//public const int xIL_Printer					=  106  ;
		//public const int xIL_Red_Minus					=  107  ;
		//public const int xIL_Refresh					=  108  ;
		//public const int xIL_Refresh_Document			=  109  ;

		//public const int xIL_Refresh_Record				=  110  ;
		//public const int xIL_Report_Histogram			=  111  ;
		//public const int xIL_Save_Blue					=  112  ;
		//public const int xIL_Save_Green					=  113  ;
		//public const int xIL_Save_Red					=  114  ;
		//public const int xIL_Search_1					=  115  ;
		//public const int xIL_Search_2					=  116  ;
		//public const int xIL_Search_Database			=  117  ;
		//public const int xIL_Smiley						=  118  ;
		//public const int xIL_Smiley_Unhappy				=  119  ;

		//public const int xIL_Stop_Document				=  120  ;
		//public const int xIL_Stop_Light_Black_Amber		=  121  ;
		//public const int xIL_Stop_Light_Black_Green		=  122  ;
		//public const int xIL_Stop_Light_Black_Off		=  123  ;
		//public const int xIL_Stop_Light_Black_Red		=  124  ;
		//public const int xIL_Top						=  125  ;
		//public const int xIL_Unlocked					=  126  ;
		//public const int xIL_Up							=  127  ;
		//public const int xIL_User_2						=  128  ;
		//public const int xIL_User_3						=  129  ;

		//public const int xIL_User_Hard_Hat_Male			=  130  ;
		//public const int xIL_View						=  131  ;
		//public const int xIL_View_Database				=  132  ;
		//public const int xIL_Wizard						=  133  ;
		//public const int xIL_Undo						=  134  ;
		//public const int xIL_Truck						=  135  ;
		//public const int xIL_UserGroup_1				=  136  ;
		//public const int xIL_Handshake					=  137  ;
		//public const int xIL_RedPlus					=  138  ;
		
		//public const int xIL_Forbid_Sign_2				=  139  ;
		//public const int xIL_No_Entry					=  140  ;
		//public const int xIL_Stop_Sign					=  141  ;
		//public const int xIL_Stop_Sign_Blank			=  142  ;
		//public const int xIL_Hourglass					=  143  ;
		//public const int xIL_Mail						=  144  ;
		//public const int xIL_SpellCheck_2				=  145  ;
		
		#endregion Imagelist Konstanten

		#region auskommentiert - Lookup Table Grid Erzeugungs Informationen 
	
		//public const int xGRD_TABLE            =  0  ; // anzuzeigende Lookup Tabelle
		//public const int xGRD_TABLE_NAME       =  1  ; // Name der anzuzeigenden Lookup Tabelle
		//public const int xGRD_PK_TYPE          =  2  ; // Daten Typ des PK 
		//public const int xGRD_AUTO_INCR        =  3  ; // Flag ob autoincrement flag für Gridtabelle setzen (NUR für Identity Felder)
					
		//// WHERE Bedingung für das Abfragen in Tabellen mit dem FK des zu löschenden Keys mit COUNT(*)
		//// WICHTIG - WHERE ist im String NICHT mit enthalten. Abhängig vom Ergebnis der Überprüfung
		//// in den FK Tabellen auf den zu löschenden Wert, können in frmGrid keine oder mehrere
		//// Bedingungen erstellt werden, die vor einem Löschvorgang überprüft werden. 
		//// Sind wie z.B. beim Nationalitäten Schlüssel mehrere FK in einer
		//// Tabelle möglich, sind die entsprechenden Strings mit OR zu verknüpfen            
		//// Daher ist es möglich, dass frmGrid.DelChkCond und frmGrid.UpdCond nicht gleich sind
		//public const int xGRD_DEL_CHK_COND     =  4  ; 

		//public const int xGRD_UPD_COND         =  5  ; // Update Condition zum Ändern und Speichern ( OHNE WHERE !!!)			
		//public const int xGRD_WHERE_BDG        =  6  ; // WHERE Einschränkung für die anzuzeigende Tabelle, wenn für bestimmte User nicht alle Sätze sichtbar sein sollen   
		//public const int xGRD_WRITE_LOG        =  7  ; // Flag, ob Logdatei schreiben  
		//public const int xGRD_USER_NAME        =  8  ; // Angemeldeter User  
		//public const int xGRD_USE_FILTER_ROW   =  9  ; // Flag, ob FilterRow Feature zu nutzen		
		//public const int xGRD_SQL_CONN_STR_VAR = 10  ; // appconfig Connection Var Name 
		//public const int xGRD_SQL_CONN_STR     = 11  ; // connection String 
		//public const int xGRD_SQL_JOIN_STRING  = 12  ; // Join String um weitere Tabellen mit entspr. Infos anzubinden
		//public const int xGRD_KILL_INSERT      = 13  ; // Flag, ob Insert Möglichkeiten im Grid abklemmen
		//public const int xGRD_COMBO_COLS       = 14  ; // ArrayList mit einem oder mehreren ArrayLists für Combos im Grid
		//public const int xGRD_AUTO_GEN_ID      = 15  ; // Flag ob ID automatisch erzeugt wird - xGRD_AUTO_INCR MUSS false sein !!
		//public const int xGRD_KILL_DELETE      = 16  ; // Flag, ob Delete Möglichkeiten im Grid abklemmen
		
		//public const int xGRD_FILTER_MAX_ROWS  = 17  ; // ggf. Zeilenbegrenzung für Grid bei großen Datenmengen
		//public const int xGRD_FILTER_SORT      = 18  ; // ggf. initial Sortierung für Grid bei großen Datenmengen
		//public const int xGRD_FILTER_WHERE     = 19  ; // ggf. zusätzlicher Filter für Grid bei großen Datenmengen
		
		#endregion
        			  
		#region auskommentiert - Konstanten für die Datenkonvertierung und Datenzugriff  

		//public const int ctCTRL_TYP              = 0 ;  // 0 Das zu füllende Control 					
		//public const int ctUPDATE_VAL            = 1 ;  // 1 InitialVal des Controls bei Update		
		//public const int ctINS_ALL_VAL           = 2 ;  // 2 InitialVal des Controls bei Insert All
		//public const int ctINS_NONE_VAL          = 3 ;  // 3 InitialVal des Controls bei Insert None
		//public const int ctINS_SOME_VAL          = 4 ;  // 4 InitialVal des Controls bei Insert Some		        
		//public const int ctTS_NAME               = 5 ;  // 5 Zuständiges Timestamp Feld + Wert
		//public const int ctUPD_COND              = 6 ;  // 6 WHERE Bedg. beim Speichern        
			
		//public const int ctSUB_ELEMENTS_NO       = 7 ;  // Grösse der Sub ArrayList

		#endregion Konstanten für die Datenkonvertierung 

		#region auskommentiert - Feldnamen für Daten Speicher Tabelle 

		//public const string FLD_DB_TABLE      = "Db_Table"     ; // enthält den Namen der jeweiligen Tabelle
		//public const string FLD_DB_FIELD      = "Db_Field"     ; // enthält den jeweiligen FeldNamen der Tabelle
		//public const string FLD_DB_VALUE      = "Db_Value"     ; // enthält den jeweiligen Wert zum FeldNamen der Tabelle
		//public const string FLD_DB_OLD_VALUE  = "Db_OldValue"  ; // enthält den jeweiligen Vorgänger Wert zum FeldNamen der Tabelle
		//public const string FLD_DB_UPDCOND    = "Db_UpdCond"   ; // enthält die jeweilige Update / Delete WHERE Bedg. der Tabelle
		//public const string FLD_DB_STRUTBL    = "Db_StruTbl"   ; // enthält die jeweilige Strukturtabelle zur Tabelle
		
		//public const string FLD_DB_CARGO      = "Db_Cargo"     ; // enthält beliebige String Werte - z.B. Infos über den zu löschenden Satz
		//public const string FLD_DB_WRITEALL   = "Db_WriteAll"  ; // Flag, ob beim Insert automatisch alle Felder aufgeführt werden sollen 
		//														 // oder eine selbst erstellte Zusammenfassung gespeichert wird (Default JA)
		//public const string FLD_DB_ORDER      = "Db_Order"     ; // int - Reihenfolge der Tabellen
																	   
		//public const string FLD_DB_SYNC_INFO  = "Db_SyncInfo"  ; // string weitere Informationen z.B. beim Löschen mehrerer zusammenhängender Tabellen        
		//public const string FLD_DB_SYNC_ID    = "Db_SyncId"    ; // object ID des zu synchronisierenden Satzes
		

		#endregion Feldnamen für Daten Speicher Tabelle 
			   
		#region auskommentiert - Feldnamen für Lookup Hilfs Tabelle

		//public const string FLD_LK_FIELD		= "Lk_Field"    ; // der jeweilige FeldName der Tabelle
		//public const string FLD_LK_DEFAULT		= "Lk_Default"  ; // der zum Feld gehörige default für die Neueingabe		
		//public const string FLD_LK_HEADER		= "Lk_Header"   ; // Grid Caption (leerer Wert - keine Anzeige)
		//public const string FLD_LK_FLAG			= "Lk_Flag"     ; // Kennunge für Feldnamen Logfelder und Time_Stamp
		//public const string FLD_LK_READONLY		= "Lk_ReadOnly" ; // Flag, ob Feld readonly
		//public const string FLD_LK_NO_EDIT		= "Lk_NoEdit"   ; // Flag, ob Feld readonly

// 		public const string FLD_LK_MASK			= "Lk_Mask"     ; // optionale Eingabemaske des Feldes
							
		#endregion Feldnamen für Lookup Hilfs Tabelle
		
		#region auskommentiert - Allgemeine Konstanten für die Datenkonvertierung 
								
		
		//public const string xWHERE                     = "EditWhereBdg" ;		
			
		//public const string xERF_AM         = "CreatedAt"     ;  
		//public const string xERF_VON        = "CreatedBy"    ;  

		//public const string xAEND_AM        = "LastChangedAt"       ;  
		//public const string xAEND_VON       = "LastChangedBy"      ;  

		#endregion Allgemeine Konstanten für die Datenkonvertierung         
							
		#region auskommentiert - Legacy code
   
 		//public const string xERF_DAT          = "erfdat"      ; // Flag für Erfassungsdatum
 		//public const string xERF_USER         = "erfuser"     ; // Flag für Erfasser
 		//public const string xCHG_DAT          = "chgdat"      ; // Flag für Änderungssdatum
 		//public const string xCHG_USER         = "chguser"     ; // Flag für Änderer

		#region auskommentiert - Kostanten für die globale ImageList / ImageFiles

		// Kostanten für die globale ImageList
		//public const int imgEXIT          =  0 ;		//       "exit.gif" ;
		//public const int imgNEXT          =  1 ;		//		 "next.bmp" ;
		//public const int imgGOTOP         =  2 ;		//		 "pl_first.bmp" ;
		//public const int imgGOBOTT        =  3 ;		//		 "pl_last.bmp" ;
		//public const int imgPREV          =  4 ;		//		 "prev.bmp" ;
		//public const int imgNEW           =  5 ;		//		 "Folder14.bmp" ;
		//public const int imgDISKGREEN     =  6 ;		//		 "DiskGreen.bmp" ;
		//public const int imgDISKRED       =  7 ;		//		 "DiskRed.bmp" ;
		//public const int imgPRINT         =  8 ;		//		 "Print.gif" ;
		//public const int imgDELETE_USER   =  9 ;		//		 "Lock4.bmp" ;
		//public const int imgFIND          = 10 ;		//		 "Find.bmp" ;
		//public const int imgGRID          = 11 ;		//		 "Grid.bmp" ;
		//public const int imgGREENCRICLE   = 12 ;		//		 "GreenCircle.gif" ;
		//public const int imgYELLOWCIRCLE  = 13 ;		//		 "YellowCircle.gif" ;
		//public const int imgBLUECIRCLE    = 14 ;		//		 "BlueCircle.gif" ;
		//public const int imgREDCIRCLE     = 15 ;		//		 "RedCircle.gif" ;
		//public const int imgREFRESH       = 16 ;		//		 "BlackCircle.gif" ;
		//public const int imgDELETE        = 17 ;		//		 "Delete.bmp" ;
		//public const int imgTREEPLUS      = 18 ;		//		 "TreePlus.bmp" ;
		//public const int imgUSER          = 19 ;		//		 "User.gif" ;
		//public const int imgREDFLAG       = 20 ;		//		 "RedFlag.gif" ;
		//public const int imgGREENFLAG     = 21 ;		//		 "Greenflag.gif" ;
		//public const int imgTOOLS         = 22 ;		//		 "tools_16.jpg" ;
		//public const int imgHELP          = 23 ;		//		 "Help5.bmp" ;
		//public const int imgEXCLAMATION   = 24 ;		//		 "Exclamation.gif" ;
		//public const int imgINFO          = 25 ;		//		 "Info_16.gif" ;
		//public const int imgPIN_RED       = 26 ;		//		 "Exit_1.bmp" ;
		//public const int imgPIN_BLUE      = 27 ;		//		 "Exit_2.bmp" ;
		//public const int imgUNDO          = 28 ;		//		 "Undo.bmp"   ;
		//public const int imgTREEMINUS     = 29 ;     //       "TreeMinus.bmp"      
		//public const int imgTRIANGLE_RED  = 30 ;     //       "Toolbox.bmp"      
		//public const int imgTRIANGLE_BLUE = 31 ;     //       "Doremi.bmp"
		//public const int imgEDIT          = 32 ;     //       "Edit.gif"
		//public const int imgCLOCK         = 33 ;     //       "Edit.gif"
		//public const int imgHOME          = 34 ;     //       "Edit.gif"
		//public const int imgSEARCH        = 35 ;    // 
		//public const int imgVIEW          = 36 ;    // 
		//public const int imgALERT_1       = 37 ;    // 
		//public const int imgALERT_2       = 38 ;    // 
		//public const int imgERROR_ROUND   = 39 ;    // 
		//public const int imgSTOP          = 40 ;    //         
		//public const int imgPENCIL        = 41 ;    //         
		//public const int imgEXIT_1        = 42 ;    // 

			  

		#endregion Kostanten für die globale ImageList / ImageFiles 

		#endregion Legacy code
			
        #endregion auskommentiert

	} // -- end public class Const

} // -- end of namespace

