using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Types.Descriptors;

namespace Tranchy.Common.HotChocolate;

public class MobileAttribute() : TranchyTagAttribute("mobile");
public class WebAttribute() : TranchyTagAttribute("web");

[AttributeUsage(
    AttributeTargets.Class |
    AttributeTargets.Struct |
    AttributeTargets.Interface |
    AttributeTargets.Enum |
    AttributeTargets.Property |
    AttributeTargets.Method |
    AttributeTargets.Field |
    AttributeTargets.Parameter,
    AllowMultiple = true)]
public class TranchyTagAttribute : DescriptorAttribute
{
    public TranchyTagAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }

    protected override void TryConfigure(IDescriptorContext context, IDescriptor descriptor, ICustomAttributeProvider element)
    {
        switch (descriptor)
        {
            case IObjectTypeDescriptor desc:
                desc.Tag(Name);
                break;

            case IInterfaceTypeDescriptor desc:
                desc.Tag(Name);
                break;

            case IUnionTypeDescriptor desc:
                desc.Tag(Name);
                break;

            case IInputObjectTypeDescriptor desc:
                desc.Tag(Name);
                break;

            case IEnumTypeDescriptor desc:
                desc.Tag(Name);
                break;

            case IObjectFieldDescriptor desc:
                desc.Tag(Name);
                break;

            case IInterfaceFieldDescriptor desc:
                desc.Tag(Name);
                break;

            case IInputFieldDescriptor desc:
                desc.Tag(Name);
                break;

            case IArgumentDescriptor desc:
                desc.Tag(Name);
                break;

            case IDirectiveArgumentDescriptor desc:
                desc.Tag(Name);
                break;

            case IEnumValueDescriptor desc:
                desc.Tag(Name);
                break;

            case ISchemaTypeDescriptor desc:
                desc.Tag(Name);
                break;

            case IFilterFieldDescriptor desc:
                desc.Directive(new Tag(Name));
                break;

            case ISortFieldDescriptor desc:
                desc.Directive(new Tag(Name));
                break;
            default:
                throw new SchemaException(
                    SchemaErrorBuilder.New()
                        .SetMessage("TagDirective Descriptor NotSupported")
                        .SetExtension("member", element)
                        .SetExtension("descriptor", descriptor)
                        .Build());
        }
    }
}