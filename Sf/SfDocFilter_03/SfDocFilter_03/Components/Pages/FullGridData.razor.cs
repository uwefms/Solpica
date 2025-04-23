using System.Collections;

using AppMainLib;

using Microsoft.AspNetCore.Components;

using SfDocFilter.BO;
using SfDocFilter.Dal;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;

namespace SfDocFilter_03.Components.Pages
{
	public partial class FullGridData  : ComponentBase
	{
		[Inject] NavigationManager? navManager { get; set; }

		[Inject] IOnBaseDal? OnBaseDal { get; set; }


		public List<OnBaseDto>? OnBaseList { get; set; } 


		private List<Object> Toolbaritems = new List<Object>() {  "ExcelExport"
																, "Print"
																, "PdfExport"
																, "CsvExport"
																, "ColumnChooser" 
		};

		SfGrid<OnBaseDto>? FullGrid;

		// Set Filter Operator
		readonly FilterSettings StartsWith	= new FilterSettings{ @Operator = Operator.StartsWith };
		readonly FilterSettings Contains	= new FilterSettings{ @Operator = Operator.Contains };


		public async Task ToolbarClick(Syncfusion.Blazor.Navigations.ClickEventArgs args)
		{
			// Praefix must be equal ID in control !!!

			if (args.Item.Id == "Grid_pdfexport")	//Id is combination of Grid's ID and itemname 		
			{
				PdfExportProperties ExportProperties = new PdfExportProperties
				{
					FileName = "Gridlist.pdf",
					PageOrientation = Syncfusion.Blazor.Grids.PageOrientation.Landscape,
				};

                await this.FullGrid!.ExportToPdfAsync(ExportProperties);
			}

			if (args.Item.Id == "Grid_excelexport")			
			{
				ExcelExportProperties ExcelProperties = new ExcelExportProperties
				{
					FileName = "Schülerliste.xlsx",
				};

                await this.FullGrid!.ExportToExcelAsync(ExcelProperties);
			}

			if (args.Item.Id == "Grid_csvexport")			
			{
				ExcelExportProperties ExcelProperties = new ExcelExportProperties
				{
					FileName = "Schülerliste.csv",
				};
								
				
                await this.FullGrid!.ExportToCsvAsync(ExcelProperties);
			}
		}

		private string Info = "-" ;		

		//public void QueryCellInfoHandler(QueryCellInfoEventArgs<OnBaseDto> args)
		//{
		//	if (args.Data.AnmeldeStatus == "ohne Zuweisung")
		//	{
		//	    args.Cell.AddClass(new string[] { "check" });
		//	}
		//}

		/// <summary>
		/// Show name 
		/// </summary>
		/// <param name="args"></param>
		//public void RowSelectHandler(RowSelectEventArgs<OnBaseDto> args)
		//{			
		//	Info1 = args.Data.Sch_FullName;			
		//}

		private async Task Reload()
		{
			var onBaseList = new OnBaseList(OnBaseDal);

			OnBaseList = await onBaseList.GetOnBaseListAsync(null);

			Info = OnBaseList.Count.ToString() + " Records ";
								
		} // end


		private void CloseWin()
		{			
			navManager!.NavigateTo("./home");
		}

		public async Task OnActionComplete(ActionEventArgs<OnBaseDto> args) 
		{
			// System.Diagnostics.Debug.WriteLine("OnActionComplete   " + args.RequestType.ToString());

			if (args.RequestType.ToString() == "Grouping")
			{
				await FullGrid!.CollapseAllGroupAsync();		

				var data = FullGrid!.CurrentViewData;

				if (data != null)
				{
					var test = (data as IEnumerable).AsQueryable();

					int nX = test.Count();

					Info = nX.ToString() + " Records ";
				}
			}
			else if (args.RequestType.ToString() == "Filtering")
			{
				try
				{	
					Info = FullGrid!.TotalItemCount.ToString() + " Records ";
				}
				catch (Exception ex)
				{
					Misc.ShowDebugInfo(false, "Error in OnActionComplete " + ex.Message );
					throw;
				}
			}			
			else
			{
				Info = FullGrid!.TotalItemCount.ToString() + " Records ";
			}

		} // end

		public FullGridData(){}

	} // end of class

} // end of namespace
