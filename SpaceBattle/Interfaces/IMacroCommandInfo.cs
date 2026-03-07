using System.Collections.Generic;

namespace SpaceBattle.Interfaces
{
    public interface IMacroCommandInfo
    {
        // Возвращает имена зависимостей команд, которые должны быть выполнены
        IEnumerable<string> GetCommandDependencies();
        // Объект, над которым выполняется операция
        object GetTarget();
    }
}