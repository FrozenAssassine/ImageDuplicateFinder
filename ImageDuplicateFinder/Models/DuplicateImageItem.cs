using System.Collections.Generic;

namespace ImageDuplicateFinder.Models;

public class DuplicateImageItem
{
    public List<string> Path;
    public string FirstItem => Path[0];
}  
