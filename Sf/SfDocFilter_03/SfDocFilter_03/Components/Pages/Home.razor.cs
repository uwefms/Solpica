using AppConst;

using Microsoft.AspNetCore.Components;

namespace SfDocFilter_03.Components.Pages
{
	public partial class Home : ComponentBase
	{
		string DataModel = Constants.DataModel!;

		string InfoText = string.Empty;

		protected override void OnInitialized()
		{
			#pragma warning disable CS0162 // Unreachable code detected
			if (Constants.IsTesting)
			{
				InfoText = $"DataForm Testing - Data-model -> {DataModel}";
			}
			else
			{		
				InfoText = "DataForm Testing";
			}

			#pragma warning restore CS0162 // Unreachable code detected

		}

		public Home(){}

	} // end of class

} // end of namespace
