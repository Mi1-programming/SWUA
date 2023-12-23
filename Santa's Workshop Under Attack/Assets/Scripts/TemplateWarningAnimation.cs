public class TemplateWarningAnimation : ConditionalAnimation
{
    public override bool Condition()
    {
        return TemplateSystem.currentTemplate == null;
    }
}
