#if UNITY_EDITOR
using System.Collections.Generic;
using LWShootDemo.BuffSystem.Tags;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEditor;

[assembly: RegisterValidationRule(typeof(BuffTagContainerValidator), Name = "BuffTagContainerValidator", Description = "Some description text.")]

public class BuffTagContainerValidator : ValueValidator<BuffTagContainer>
{
    // Introduce serialized fields here to make your validator
    // configurable from the validator window under rules.
    public int SerializedConfig;

    protected override void Validate(ValidationResult result)
    {
        var buffTagContainer = this.Value;
        
        if ( buffTagContainer.Tags.Count != new HashSet<BuffTag>(buffTagContainer.Tags).Count)
        {
            result.AddError("Tags中存在重复的BuffTag");
        }
    }
}
#endif
