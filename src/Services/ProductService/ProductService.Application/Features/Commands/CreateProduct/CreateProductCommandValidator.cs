using FluentValidation;

namespace ProductService.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

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
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.AttributeValueId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.AttributeName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
            RuleFor(x => x.AttributeValueName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
        }
    }

    public class ProductPicturesCommandValidator : AbstractValidator<ProductPicturesCommand>
    {
        public ProductPicturesCommandValidator()
        {
            RuleFor(x => x.IsApproved)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.PictureUrl)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");
        }
    }

    public class ProductSellersCommandValidator : AbstractValidator<ProductSellersCommand>
    {
        public ProductSellersCommandValidator()
        {
            RuleFor(x => x.SellerId)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}