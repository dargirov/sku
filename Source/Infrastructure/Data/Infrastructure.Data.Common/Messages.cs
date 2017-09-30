using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.Common
{
    public class Messages
    {
        private enum MessageType
        {
            Error,
            Success,
            Warning
        }

        private readonly IList<(MessageType type, string message)> _messages;

        public Messages() => _messages = new List<(MessageType type, string message)>();

        private void Add(MessageType type, string message) => _messages.Add((type: type, message: message));

        public void AddError(string message) => Add(MessageType.Error, message);

        public void AddWarning(string message) => Add(MessageType.Warning, message);

        public void AddSuccess(string message) => Add(MessageType.Success, message);

        public bool HasErrors => _messages.Any(x => x.type == MessageType.Error);
        public bool HasWarnings => _messages.Any(x => x.type == MessageType.Warning);
        public bool HasSuccesses => _messages.Any(x => x.type == MessageType.Success);

        public List<string> GetErrors() => _messages.Where(x => x.type == MessageType.Error).Select(x => x.message).ToList();
        public List<string> GetWarnings() => _messages.Where(x => x.type == MessageType.Warning).Select(x => x.message).ToList();
        public List<string> GetSuccesses() => _messages.Where(x => x.type == MessageType.Success).Select(x => x.message).ToList();
    }
}
