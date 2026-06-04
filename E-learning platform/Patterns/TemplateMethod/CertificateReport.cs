namespace E_learning_platform.Patterns.TemplateMethod
{
    public class CertificateReport : ReportGenerator
    {
        private readonly string _studentName;
        private readonly string _courseName;

        public CertificateReport(string studentName, string courseName)
        {
            _studentName = studentName;
            _courseName = courseName;
        }

        protected override string PrintHeader()
        {
            return "******************************************\n" +
                   "*      CERTIFICATE OF COMPLETION         *";
        }

        protected override string PrintContent()
        {
            return $"This certifies that {_studentName}\nhas successfully completed the course:\n{_courseName}";
        }

        protected override string PrintFooter()
        {
            return "******************************************\nVerification Code: CERT-" + System.Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }
}
