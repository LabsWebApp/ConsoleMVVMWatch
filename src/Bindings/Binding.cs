namespace ConsoleMVVMWatch.Bindings
{
    internal class Binding
    {
        public string DataContextPropertyName { get; }

        public Binding(string dataContextPropertyName)
            => DataContextPropertyName = dataContextPropertyName;
    }
}