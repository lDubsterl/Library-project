using Library.Domain.Interfaces;

namespace Library.Domain.Entities
{
	public class RefreshToken: IEntity
	{
		public int  Id { get; set; }
		public int UserId { get; set; }
		public string TokenHash { get; set; }
		public string TokenSalt { get; set; }
		public DateTime Ts {  get; set; }
		public DateTime ExpiryDate { get; set; }
		public User User { get; set; }
	}
}
