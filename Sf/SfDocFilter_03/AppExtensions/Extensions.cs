namespace AppExtensions
{
	public static class Extensions
	{
		#region Vermeidung von async void

		/// <summary>
		/// Methode, um async void zu vermeiden
		/// Aufruf: <awaitable Method>.AwaitFull(Completed, HandleError) es müssen 2 Methoden Completed(){}, HandleError(){} vorhanden sein
		/// </summary>
		/// <param name="task"></param>
		/// <param name="completedCallBack"></param>
		/// <param name="errorCallBack"></param>
		public async static void AwaitFull(this Task task, Action completedCallBack, Action<Exception> errorCallBack)
		{
			try
			{
				await task;
				completedCallBack?.Invoke();
			}
			catch (Exception ex)
			{
				errorCallBack?.Invoke(ex);
			}
			
		}

		/// <summary>
		/// Methode, um async void zu vermeiden
		/// Aufruf: <awaitable Method>.AwaitFull(HandleError) es müssen 1 Methode HandleError(){} vorhanden sein
		/// </summary>
		/// <param name="task"></param>
		/// <param name="errorCallBack"></param>
		public async static void Await(this Task task, Action<Exception> errorCallBack)		
		{
			try
			{
				await task;				
			}
			catch (Exception ex)
			{
				errorCallBack?.Invoke(ex);
			}
			
		} // end

		#region Beispiel 

		/*
		Beispiel:

		private void button1_Click(object sender, EventArgs e)
		{
			DoSomething().Await(HandleError);
			DoSomething1().AwaitFull(Completed1, HandleError);
		}

		private void HandleError(Exception ex)
		{
			Msg = ex.Message;
			this.textBox1.Text = Msg;	
		}

		private void Completed1()
		{
			Msg = "Completed DoSomething_1";
			this.textBox1.Text = Msg;
		}

		async Task DoSomething()
		{
			Msg = "Changed in DoSomething";
			this.textBox1.Text = Msg;	

			await Task.Delay(2000);			
		}

		async Task DoSomething1()
		{
			await Task.Delay(2000);
			Msg = "Changed in DoSomething_1";
			this.textBox1.Text = Msg;							
		}

		*/

		#endregion Beispiel 

		#endregion Vermeidung von async void

	} // end of class

} // end of namespace