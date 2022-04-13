namespace WebStore.DTO
{
    public class SectionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Order { get; set; }

        public List<SectionDTO> ChildSections { get; set; } = new();
    }
}
