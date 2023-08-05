namespace CDG.Web.Models.KeyCategory
{
    public class KeyCategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string PictureUri { get; set; } = "no_img";
    }
}