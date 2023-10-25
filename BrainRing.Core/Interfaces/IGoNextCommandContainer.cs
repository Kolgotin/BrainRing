using System.Windows.Input;

namespace BrainRing.Core.Interfaces;

public interface IGoNextCommandContainer
{
    ICommand? GoNextCommand { get; set; }
}