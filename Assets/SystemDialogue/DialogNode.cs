using System.Collections.Generic;
public class DialogNode
{
    public int Id {  get; private set; }
    public string ShortName { get; set; }
    public string Name { get; set; }
    public string Message { get; set; }
    public List<DialogNode> children {  get; set; } = new List<DialogNode>();
    public int? ActionId { get; set; }
}