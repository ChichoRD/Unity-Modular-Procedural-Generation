public interface IGenerationModifier<in TDataIn, out  TDataOut>
    where TDataIn : IGenerationData
    where TDataOut : IGenerationData
{
    TDataOut Modify(TDataIn generationData);
}
