using ReenbitTestTask24.DTO;

namespace ReenbitTestTask24.Interfaces
{
    public interface IMessageService
    {
        public Task<List<MessageDTO>> GetMessages();


    }
}
