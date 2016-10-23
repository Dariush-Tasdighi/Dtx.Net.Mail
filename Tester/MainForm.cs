using System.Linq;

namespace MyApplication
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
		}

		private void btnSend_Click(object sender, System.EventArgs e)
		{
			System.Net.Mail.MailAddress oRecipient =
				new System.Net.Mail.MailAddress(address: txtTo.Text);

			Dtx.Result oResult = Dtx.Net.Mail.Utility.Send
				(recipient: oRecipient,
				subject: txtSubject.Text,
				body: txtBody.Text,
				priority: System.Net.Mail.MailPriority.Normal);

			if (oResult.Success)
			{
				System.Windows.Forms.MessageBox.Show("نامه الکترونیکی با موفقیت ارسال گردید");
			}
			else
			{
				string strMessage = string.Empty;

				System.Exception oException = oResult.Exception;

				while (oException != null)
				{
					if (strMessage != string.Empty)
					{
						strMessage += System.Environment.NewLine;
					}

					strMessage += oException.Message;

					oException = oException.InnerException;
				}

				System.Windows.Forms.MessageBox.Show(strMessage);
			}
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			txtTo.Text = string.Empty;
			txtBody.Text = string.Empty;
			txtSubject.Text = string.Empty;
		}

		private void btnAbout_Click(object sender, System.EventArgs e)
		{
			string strMessage =
				"DTX NET MAIL - " +
				System.Environment.NewLine +
				System.Environment.NewLine +
				"Mr Dariush Tasdighi" +
				System.Environment.NewLine +
				System.Environment.NewLine +
				"+98-912-108-7461" +
				System.Environment.NewLine +
				"DariushT@GMail.com" +
				System.Environment.NewLine +
				"Telegram: @IranianExperts"
				;

			System.Windows.Forms.MessageBox.Show(strMessage);
		}
	}
}
