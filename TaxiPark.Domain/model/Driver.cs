using System.ComponentModel.DataAnnotations;

namespace TaxiPark.Domain.model;
class Driver
{
    // <summary>
    // Индификатор водителя
    // </summary>
    [Key]
    public required int Id { get; set; }
    //<summary>
    //Имя водителя 
    //</summary>
    public string? LastName { get; set; }

    //<summary>
    //Фамилия водителя 
    //</summary>
    public string? FirstName { get; set; }

    //<summary>
    //Отчество водителя 
    //</summary>
    public string? Patronymic { get; set; }
    //<summary>
    //Телефон водителя
    //</summary>
    public int? PhoneNumber { get; set; }
    //<summary>
    //Серия паспорта
    //</summary>
    public int? PassportSeries { get; set; }
    //<summary>
    //Номер паспорта
    //</summary>
    public int? PassportNumber { get; set; }
}

