namespace Workouts.Mail.DummyTemplate
{
    public class Templates
    {
        public string GetVerificationTemplate(string userName, int verificationCode)
        {
            return
                " <div style=\"display:flex; flex-direction: column;\">\r\n " +
                $"<h4> Merhaba {userName}, </h4>\r\n " +
                "<p>Sitemiz kayıt olduğun için çok mutluyuz.</p>\r\n" +
                $"<p>Aktivaston Kodun : <b> {verificationCode} </b></p>\r\n   " +
                "<p>İyi günler dileriz.</p>\r\n   " +
                "</div>";
        }
    }
}
