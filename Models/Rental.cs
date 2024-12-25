﻿namespace ComicSystem.Models;

public class Rental 
{
    public int RentalID { get; set; }
    public int CustomerID { get; set; }
    public DateTime RentalDate { get; set; }  
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<RentalDetail> RentalDetails { get; set; }
}