namespace E_learning_platform.Patterns.TemplateMethod
{
    public class InvoiceReport : ReportGenerator
    {
        private readonly string _customerName;
        private readonly decimal _amount;

        public InvoiceReport(string customerName, decimal amount)
        {
            _customerName = customerName;
            _amount = amount;
        }

        protected override string PrintHeader()
        {
            return "TAX INVOICE #INV-" + System.DateTime.Now.Ticks.ToString().Substring(10);
        }

        protected override string PrintContent()
        {
            return "Bill To: " + _customerName + "\n" +
                   "Description: E-learning Course Subscription\n" +
                   "Total Amount: $" + _amount;
        }
    }
}
