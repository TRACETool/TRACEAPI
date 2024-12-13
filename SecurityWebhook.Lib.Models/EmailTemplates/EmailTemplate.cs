namespace SecurityWebhook.Lib.Models.EmailTemplates
{
    public class EmailTemplate
    {
        public const string NotifyEmail = @"<p>Dear [UserName],</p>

            <p>We wanted to notify you about the following:</p>

            <p><strong>[Subject]</strong></p>
            <p>[Details]</p>

            <p>You can take action or learn more by clicking the button below:</p>
            <p style=""text-align: center;"">
                <a href=""[URL]"" 
                   style=""display: inline-block; background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;"">
                   Take Action
                </a>
            </p>

            <p>If you have any questions, feel free to <a href=""mailto:yourmail@gmail.com"">contact us</a>.</p>

            <p>Best regards,<br>The SSCP Team</p>";

        public const string NotifyAnomalyEmail = @"<p>Dear [UserName],</p>

            <p>We wanted to notify you about the following possible anomalies in latest commits from [RepoName]:</p>

            <p><strong>[Normal]</strong></p>
	    <p><strong>[Slight]</strong></p>
            <p><strong>[Moderate]</strong></p>
            <p><strong>[High]</strong></p>
            

            <p>Please review these anomalies by logging into your SSCP account!</p>
            

            <p>If you have any questions, feel free to <a href=""mailto:yourmail@gmail.com"">contact us</a>.</p>

            <p>Best regards,<br>The SSCP Team</p>";
    }

}