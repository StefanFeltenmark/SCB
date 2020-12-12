namespace SCB.Domain
{
    public class SCBQueryItem
    {
        // begin DTO props
        public string code;
        public SCBSelection selection;
        // end DTO props

        private SCBVariable _var;

        public SCBQueryItem(SCBVariable variable)
        {
            Var = variable;
            code = variable.code;
            selection = new SCBSelection(variable);
        }

        public SCBVariable Var
        {
            get => _var;
            set => _var = value;
        }
    }
}
