using FluentValidation;
using ProductService.Application.Features.Commands.ProductCommon;

namespace ProductService.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{Name} is required.")
                .MaximumLength(500).WithMessage("{Name} must not exceed 500 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{Description} is required.")
                .MaximumLength(500).WithMessage("{Description} must not exceed 500 characters.");

            When(x => x.ProductAttributes != null, () =>
            {
                RuleForEach(x => x.ProductAttributes)
                .SetValidator(new ProductAttributesCommandValidator());
            });
            When(x => x.ProductPictures != null, () =>
            {
                RuleForEach(x => x.ProductPictures)
                .SetValidator(new ProductPicturesCommandValidator());
            });

            When(x => x.ProductSellers != null, () =>
            {
                RuleForEach(x => x.ProductSellers)
                .SetValidator(new ProductSellersCommandValidator());
            });
        }
    }

    public class ProductAttributesCommandValidator : AbstractValidator<ProductAttributesCommand>
    {
        public ProductAttributesCommandValidator()
        {
            RuleFor(x => x.AttributeId)
                .NotEmpty().WithMessage("{AttributeId} is required.");
            RuleFor(x => x.AttributeValueId)
                .NotEmpty().WithMessage("{AttributeValueId} is required.");
            RuleFor(x => x.AttributeName)
                .NotEmpty().WithMessage("{AttributeName} is required.")
                .MaximumLength(500).WithMessage("{AttributeName} must not exceed 500 characters.");
            RuleFor(x => x.AttributeValueName)
                .NotEmpty().WithMessage("{AttributeValueName} is required.")
                .MaximumLength(500).WithMessage("{AttributeValueName} must not exceed 500 characters.");
        }
    }

    public class ProductPicturesCommandValidator : AbstractValidator<ProductPicturesCommand>
    {
        public ProductPicturesCommandValidator()
        {
            RuleFor(x => x.PictureUrl)
                .NotEmpty().WithMessage("{PictureUrl} is required.")
                .MaximumLength(500).WithMessage("{PictureUrl} must not exceed 500 characters.");
        }
    }

    public class ProductSellersCommandValidator : AbstractValidator<ProductSellersCommand>
    {
        public ProductSellersCommandValidator()
        {
            RuleFor(x => x.SellerId)
                .NotEmpty().WithMessage("{SellerId} is required.");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("{Price} is required.");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("{Quantity} is required.");
        }
    }
}