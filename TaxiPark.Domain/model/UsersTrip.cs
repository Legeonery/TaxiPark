using System.ComponentModel.DataAnnotations;

namespace TaxiPark.Domain.model;
//Класс связывающий пользователей и поездки
class UsersTrip
{
    /// <summary>
    /// Идентификатор связи
    /// </summary>
    [Key]
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор юзера
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    /// Навигейшен юзера
    /// </summary>
    public virtual User? User { get; set; }

    /// <summary>
    /// Идентификатор поездки
    /// </summary>
    public required int TripId { get; set; }

    /// <summary>
    /// Навигейшен поездки
    /// </summary>
    public virtual Trip? Trip { get; set; }
}

