using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AppMainLib;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

using SfDocFilter.BO;
using SfDocFilter.Dal;
using SfDocFilter.Dal.Models;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;


namespace SfDocFilter_03.Components.Pages
{
	public partial class DataFormGridFilter
	{
        [Inject] IOnBaseDal? OnBaseDal { get; set; }

        [Inject] SfDialogService? DialogService {get; set;}  

        private SearchDetails SearchDetails = new SearchDetails();

        public IEnumerable<OnBaseDto>? OnBaseList { get; set; } 

        SfGrid<OnBaseDto>? ResultGrid;

		// Set Filter Operator
		readonly FilterSettings StartsWith	= new FilterSettings{ @Operator = Operator.StartsWith };
		readonly FilterSettings Contains	= new FilterSettings{ @Operator = Operator.Contains };

        private string Info = "-" ;		

        // private bool ShowGrid = false;
        private bool ShowModal = false;

        private async Task SubmitForm(object args)
        {
			// create a filter object from the form data
            FilterDto? oFilter = new FilterDto()
            {
                Last_name       = SearchDetails.Last_name,
                First_name      = SearchDetails.First_name,
                Country	        = SearchDetails.Country,
                City            = SearchDetails.City,
                Marital_status  = SearchDetails.Marital_status,
                State		    = SearchDetails.State,
                First_name1	    = SearchDetails.First_name1,
                Last_name1	    = SearchDetails.Last_name1,

			};

			var onBaseList = new OnBaseList(OnBaseDal);

			OnBaseList = await onBaseList.GetOnBaseListAsync(oFilter);

            int nCount = OnBaseList.Count();

			Info = nCount.ToString() + " Records ";

            if (nCount > 0)
            {
                ShowModal = true; // Open the modal
            }
            else
            {
                await DialogService!.AlertAsync("No Data for this filter", "Search failed" , new DialogOptions()
 {
                AllowDragging = true,
            });
				// ShowGrid = false; // Hide the grid if no records found
			}


			// ShowModal = true; // Open the modal
            // Implementation for form submission
            // Add your form submission logic here
        }

        public async Task OnActionComplete(ActionEventArgs<OnBaseDto> args) 
		{
			// System.Diagnostics.Debug.WriteLine("OnActionComplete   " + args.RequestType.ToString());

			if (args.RequestType.ToString() == "Grouping")
			{
				await ResultGrid!.CollapseAllGroupAsync();		

				var data = ResultGrid!.CurrentViewData;

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
					Info = ResultGrid!.TotalItemCount.ToString() + " Records ";
				}
				catch (Exception ex)
				{
					Misc.ShowDebugInfo(false, "Error in OnActionComplete " + ex.Message );
					throw;
				}
			}			
			else
			{
				Info = ResultGrid!.TotalItemCount.ToString() + " Records ";
			}

		} // end

        public class States
        {
            public string? Name { get; set; }
            public string? Code { get; set; }
        }

        List<States> State = new List<States>
        {
            new States() { Name = "No Filter"       , Code = "" },
            new States() { Name = "Alabama"         , Code = "Alabama" },
            new States() { Name = "Alaska"          , Code = "Alaska" },
            new States() { Name = "Arizona"         , Code = "Arizona" },
            new States() { Name = "Arkansas"        , Code = "Arkansas" },
            new States() { Name = "California"      , Code = "California" },
            new States() { Name = "Colorado"        , Code = "Colorado" },
            new States() { Name = "Connecticut"     , Code = "Connecticut" },
            new States() { Name = "Delaware"        , Code = "Delaware" },
            new States() { Name = "Florida"         , Code = "Florida" },
            new States() { Name = "Georgia"         , Code = "Georgia" },
            new States() { Name = "Hawaii"          , Code = "Hawaii" },
            new States() { Name = "Idaho"           , Code = "Idaho" },
            new States() { Name = "Illinois"        , Code = "Illinois" },
            new States() { Name = "Indiana"         , Code = "Indiana" },
            new States() { Name = "Iowa"            , Code = "Iowa" },
            new States() { Name = "Kansas"          , Code = "Kansas" },
            new States() { Name = "Kentucky"        , Code = "Kentucky" },
            new States() { Name = "Louisiana"       , Code = "Louisiana" },
            new States() { Name = "Maine"           , Code = "Maine" },
            new States() { Name = "Maryland"        , Code = "Maryland" },
            new States() { Name = "Massachusetts"   , Code = "Massachusetts" },
            new States() { Name = "Michigan"        , Code = "Michigan" },
            new States() { Name = "Minnesota"       , Code = "Minnesota" },
            new States() { Name = "Mississippi"     , Code = "Mississippi" },
            new States() { Name = "Missouri"        , Code = "Missouri" },
            new States() { Name = "Montana"         , Code = "Montana" },
            new States() { Name = "Nebraska"        , Code = "Nebraska" },
            new States() { Name = "Nevada"          , Code = "Nevada" },
            new States() { Name = "New Hampshire"   , Code = "New Hampshire" },
            new States() { Name = "New Jersey"      , Code = "New Jersey" },
            new States() { Name = "New Mexico"      , Code = "New Mexico" },
            new States() { Name = "New York"        , Code = "New York" },
            new States() { Name = "North Carolina"  , Code = "North Carolina" },
            new States() { Name = "North Dakota"    , Code = "North Dakota" },
            new States() { Name = "Ohio"            , Code = "Ohio" },
            new States() { Name = "Oklahoma"        , Code = "Oklahoma" },
            new States() { Name = "Oregon"          , Code = "Oregon" },
            new States() { Name = "Pennsylvania"    , Code = "Pennsylvania" },
            new States() { Name = "Rhode Island"    , Code = "Rhode Island" },
            new States() { Name = "South Carolina"  , Code = "South Carolina" },
            new States() { Name = "South Dakota"    , Code = "South Dakota" },
            new States() { Name = "Tennessee"       , Code = "Tennessee" },
            new States() { Name = "Texas"           , Code = "Texas" },
            new States() { Name = "Utah"            , Code = "Utah" },
            new States() { Name = "Vermont"         , Code = "Vermont" },
            new States() { Name = "Virginia"        , Code = "Virginia" },
            new States() { Name = "Washington"      , Code = "Washington" },
            new States() { Name = "West Virginia"   , Code = "West Virginia" },
            new States() { Name = "Wisconsin"       , Code = "Wisconsin" },
            new States() { Name = "Wyoming"         , Code = "Wyoming" },
        };

		public DataFormGridFilter(){}
	}
}
