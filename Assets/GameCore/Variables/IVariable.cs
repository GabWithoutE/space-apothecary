namespace GameCore.Variables
{
    public interface IVariable<TType>
    {
        TType GetValue();
        void SetValue(TType value);
        void SetValue(IVariable<TType> value);
        void ApplyChange(TType amount);
        void ApplyChange(IVariable<TType> amount);
    }
}
