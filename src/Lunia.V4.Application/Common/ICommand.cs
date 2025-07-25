using LanguageExt;
using LanguageExt.Common;
using MediatR;

namespace Lunia.V4.Application.Common;

public interface ICommand : IRequest<Option<Error>>;

public interface ICommand<TResult> : IRequest<Either<Error, TResult>>;