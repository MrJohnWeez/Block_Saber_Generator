namespace MJW.Conversion
{
    public struct ConversionProgress
    {
        public float value;
        public string message;


        public ConversionProgress(float value)
        {
            this.value = value;
            message = "";
        }

        public ConversionProgress(string message)
        {
            value = 0;
            this.message = message;
        }

        public ConversionProgress(float value, string message)
        {
            this.value = value;
            this.message = message;
        }
    }
}
