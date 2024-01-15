namespace Quiz.Core.Interfaces.Database
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
