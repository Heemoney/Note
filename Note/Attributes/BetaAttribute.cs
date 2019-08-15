namespace Note.Attributes
{
    [Author("Manu Puduvalli", Version = 1.0)]
    [System.AttributeUsage(System.AttributeTargets.Class |
                           System.AttributeTargets.Struct |
                           System.AttributeTargets.Method |
                           System.AttributeTargets.Interface |
                           System.AttributeTargets.Enum)]
    internal class Beta : System.Attribute {
        private string Msg { get; set; }

        internal Beta()
        {

        }
        internal Beta(string msg)
        {
            this.Msg = msg;
        }
    }

    [Author("Manu Puduvalli", Version = 1.0)]
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct |
                       System.AttributeTargets.Method |
                       System.AttributeTargets.Interface |
                       System.AttributeTargets.Enum)]
    [System.Obsolete("Strong Beta's are not meant to be implemented when marked")]
    internal class StrongBeta : System.Attribute
    {
        private string Msg { get; set; }
        internal StrongBeta()
        {

        }
        internal StrongBeta(string msg)
        {
            this.Msg = msg;
        }
    }
}
