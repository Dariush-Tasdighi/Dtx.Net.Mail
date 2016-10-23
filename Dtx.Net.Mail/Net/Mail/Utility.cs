using System.Linq;

namespace Dtx.Net.Mail
{
	public static class Utility
	{
		static Utility()
		{
		}

		private static IMailSettings GetMailSettings()
		{
			IMailSettings oMailSettings = new MailSettings();

			oMailSettings.BccAddresses = ApplicationSettings.BccAddresses;
			oMailSettings.SmtpUsername = ApplicationSettings.SmtpUsername;
			oMailSettings.SmtpPassword = ApplicationSettings.SmtpPassword;
			oMailSettings.SenderDisplayName = ApplicationSettings.SenderDisplayName;
			oMailSettings.SupportDisplayName = ApplicationSettings.SupportDisplayName;
			oMailSettings.SenderEmailAddress = ApplicationSettings.SenderEmailAddress;
			oMailSettings.SupportEmailAddress = ApplicationSettings.SupportEmailAddress;
			oMailSettings.EmailSubjectTemplate = ApplicationSettings.EmailSubjectTemplate;
			oMailSettings.SmtpClientHostAddress = ApplicationSettings.SmtpClientHostAddress;

			oMailSettings.SmtpClientTimeout = ApplicationSettings.SmtpClientTimeout;
			oMailSettings.SmtpClientPortNumber = ApplicationSettings.SmtpClientPortNumber;

			oMailSettings.SmtpClientEnableSsl = ApplicationSettings.SmtpClientEnableSsl;
			oMailSettings.UseDefaultCredentials = ApplicationSettings.UseDefaultCredentials;

			return (oMailSettings);
		}

		/// <summary>
		/// تبديل متن به حالتی که برای ايميل مناسب گردد
		/// </summary>
		/// <param name="text">متن</param>
		public static string ConvertTextForEmailBody(string text)
		{
			if (text == null)
			{
				return (string.Empty);
			}

			text =
				text
				.Replace(System.Convert.ToChar(13).ToString(), "<br />") // Return Key.
				.Replace(System.Convert.ToChar(10).ToString(), string.Empty) // Return Key.
				.Replace(System.Convert.ToChar(9).ToString(), "&nbsp;&nbsp;&nbsp;&nbsp;"); // TAB Key.

			return (text);
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		/// <param name="mailSettings">تنظیمات</param>
		public static Dtx.Result Send
			(
				string subject,
				string body,
				IMailSettings mailSettings = null
			)
		{
			return (Send
				(null,
				null,
				subject,
				body,
				System.Net.Mail.MailPriority.High,
				null,
				System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSettings));
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="recipient">دريافت کننده</param>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		/// <param name="priority">اهميت</param>
		/// <param name="mailSettings"></param>
		/// <param name="mailSettings">تنظیمات</param>
		public static Dtx.Result Send
			(
				System.Net.Mail.MailAddress recipient,
				string subject,
				string body,
				System.Net.Mail.MailPriority priority,
				IMailSettings mailSettings = null
			)
		{
			// **************************************************
			System.Net.Mail.MailAddressCollection
				oRecipients = new System.Net.Mail.MailAddressCollection();

			oRecipients.Add(recipient);
			// **************************************************

			return (Send
				(null,
				oRecipients,
				subject,
				body,
				priority,
				null,
				System.Net.Mail.DeliveryNotificationOptions.Never,
				mailSettings));
		}

		/// <summary>
		/// ارسال نامه الکترونيکی
		/// </summary>
		/// <param name="sender">فرستنده</param>
		/// <param name="recipients">گيرندگان</param>
		/// <param name="subject">موضوع</param>
		/// <param name="body">شرح</param>
		/// <param name="priority">اهميت</param>
		/// <param name="attachmentPathNames">پيوست ها</param>
		/// <param name="deliveryNotification">رسيد ارسال</param>
		/// <param name="mailSettings">تنظیمات</param>
		public static Dtx.Result Send
			(
				System.Net.Mail.MailAddress sender,
				System.Net.Mail.MailAddressCollection recipients,
				string subject,
				string body,
				System.Net.Mail.MailPriority priority,
				System.Collections.Generic.List<string> attachmentPathNames,
				System.Net.Mail.DeliveryNotificationOptions deliveryNotification,
				IMailSettings mailSettings = null
			)
		{
			Dtx.Result oResult = new Result();

			if (mailSettings == null)
			{
				mailSettings = GetMailSettings();
			}

			// **************************************************
			System.Net.Mail.MailAddress oSender = null;
			System.Net.Mail.SmtpClient oSmtpClient = null;
			System.Net.Mail.MailMessage oMailMessage = null;
			// **************************************************

			try
			{
				// **************************************************
				// *** Mail Message Configuration *******************
				// **************************************************
				oMailMessage = new System.Net.Mail.MailMessage();

				// **************************************************
				oMailMessage.To.Clear();
				oMailMessage.CC.Clear();
				oMailMessage.Bcc.Clear();
				oMailMessage.Attachments.Clear();
				oMailMessage.ReplyToList.Clear();
				// **************************************************

				// **************************************************
				if (sender != null)
				{
					oSender = sender;
				}
				else
				{
					if (string.IsNullOrEmpty(mailSettings.SenderDisplayName))
					{
						oSender =
							new System.Net.Mail.MailAddress
								(address: mailSettings.SenderEmailAddress,
								displayName: mailSettings.SenderEmailAddress,
								displayNameEncoding: System.Text.Encoding.UTF8);
					}
					else
					{
						oSender =
							new System.Net.Mail.MailAddress
								(address: mailSettings.SenderEmailAddress,
								displayName: mailSettings.SenderDisplayName,
								displayNameEncoding: System.Text.Encoding.UTF8);
					}
				}

				oMailMessage.From = oSender;
				oMailMessage.Sender = oSender;

				// Note: Below Code Obsoleted in .NET 4.0
				//oMailMessage.ReplyTo = oSender;

				oMailMessage.ReplyToList.Add(oSender);
				// **************************************************

				if (recipients == null)
				{
					System.Net.Mail.MailAddress oMailAddress = null;

					if (string.IsNullOrEmpty(mailSettings.SupportDisplayName))
					{
						oMailAddress =
							new System.Net.Mail.MailAddress
								(address: mailSettings.SupportEmailAddress,
								displayName: mailSettings.SupportEmailAddress,
								displayNameEncoding: System.Text.Encoding.UTF8);
					}
					else
					{
						oMailAddress =
							new System.Net.Mail.MailAddress
								(address: mailSettings.SupportEmailAddress,
								displayName: mailSettings.SupportDisplayName,
								displayNameEncoding: System.Text.Encoding.UTF8);
					}

					oMailMessage.To.Add(oMailAddress);
				}
				else
				{
					// Note: Wrong Usage!
					// oMailMessage.To = recipients;

					foreach (System.Net.Mail.MailAddress oMailAddress in recipients)
					{
						oMailMessage.To.Add(oMailAddress);
					}
				}

				if (string.IsNullOrEmpty(mailSettings.BccAddresses) == false)
				{
					mailSettings.BccAddresses =
						mailSettings.BccAddresses
						.Replace(";", ",")
						.Replace("|", ",")
						.Replace("،", ",");

					string[] strBccAddresses =
						mailSettings.BccAddresses.Split(',');

					foreach (string strBccAddress in strBccAddresses)
					{
						bool blnFound =
							oMailMessage.To
							.Where(current => string.Compare(current.Address, strBccAddress, true) == 0)
							.Any();

						if (blnFound == false)
						{
							blnFound =
								oMailMessage.Bcc
								.Where(current => string.Compare(current.Address, strBccAddress, true) == 0)
								.Any();

							if (blnFound == false)
							{
								System.Net.Mail.MailAddress oMailAddress =
									new System.Net.Mail.MailAddress(address: strBccAddress);

								oMailMessage.Bcc.Add(item: oMailAddress);
							}
						}
					}

					// Note: [BccAddresses] must be separated with comma character (",")
					//oMailMessage.Bcc.Add(mailSettings.BccAddresses);
				}

				// **************************************************
				oMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

				if (string.IsNullOrEmpty(mailSettings.EmailSubjectTemplate))
				{
					oMailMessage.Subject = subject;
				}
				else
				{
					oMailMessage.Subject =
						string.Format(mailSettings.EmailSubjectTemplate, subject);
				}
				// **************************************************

				// **************************************************
				oMailMessage.Body = body;
				oMailMessage.IsBodyHtml = true;
				oMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
				// **************************************************

				// **************************************************
				oMailMessage.Priority = priority;
				oMailMessage.DeliveryNotificationOptions = deliveryNotification;
				// **************************************************

				if ((attachmentPathNames != null) && (attachmentPathNames.Count > 0))
				{
					foreach (string strAttachmentPathName in attachmentPathNames)
					{
						if (System.IO.File.Exists(strAttachmentPathName))
						{
							System.Net.Mail.Attachment oAttachment =
								new System.Net.Mail.Attachment(strAttachmentPathName);

							oMailMessage.Attachments.Add(oAttachment);
						}
					}
				}

				// **************************************************
				oMailMessage.Headers.Add("Dtx.Net.Mail_Version", "1.0.6");
				oMailMessage.Headers.Add("Dtx.Net.Mail_Date", "1395/08/02");
				oMailMessage.Headers.Add("Dtx.Net.Mail_Author", "Mr. Dariush Tasdighi");
				oMailMessage.Headers.Add("Dtx.Net.Mail_Url", "http://www.IranianExperts.ir");
				// **************************************************
				// *** /Mail Message Configuration ******************
				// **************************************************

				// **************************************************
				// *** Smtp Client Configuration ********************
				// **************************************************
				oSmtpClient = new System.Net.Mail.SmtpClient();

				// **************************************************
				oSmtpClient.Port = mailSettings.SmtpClientPortNumber;
				oSmtpClient.Timeout = mailSettings.SmtpClientTimeout;
				oSmtpClient.EnableSsl = mailSettings.SmtpClientEnableSsl;
				// **************************************************

				oSmtpClient.DeliveryMethod =
					System.Net.Mail.SmtpDeliveryMethod.Network;

				oSmtpClient.Host =
					mailSettings.SmtpClientHostAddress;

				// **************************************************
				// **************************************************

				// **************************************************
				oSmtpClient.UseDefaultCredentials = mailSettings.UseDefaultCredentials;

				if (mailSettings.UseDefaultCredentials == false)
				{
					System.Net.NetworkCredential oNetworkCredential =
						new System.Net.NetworkCredential
							(userName: mailSettings.SmtpUsername, password: mailSettings.SmtpPassword);

					oSmtpClient.Credentials = oNetworkCredential;
				}
				// **************************************************
				// *** /Smtp Client Configuration *******************
				// **************************************************

				oSmtpClient.Send(oMailMessage);

				oResult.Success = true;
			}
			catch (System.Exception ex)
			{
				oResult.Success = false;

				oResult.Exception = ex;
			}
			finally
			{
				if (oMailMessage != null)
				{
					oMailMessage.Dispose();
					oMailMessage = null;
				}

				if (oSmtpClient != null)
				{
					oSmtpClient.Dispose();
					oSmtpClient = null;
				}
			}

			return (oResult);
		}
	}
}
