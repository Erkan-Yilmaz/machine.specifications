using System.Collections.Generic;

using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.UnitTestExplorer;

using Machine.Specifications.ReSharperRunner.Factories;
using Machine.Specifications.ReSharperRunner.Presentation;

namespace Machine.Specifications.ReSharperRunner.Explorers.ElementHandlers
{
  internal class ContextSpecificationElementHandler : IElementHandler
  {
    readonly SpecificationFactory _specificationFactory;

    public ContextSpecificationElementHandler(SpecificationFactory specificationFactory)
    {
      _specificationFactory = specificationFactory;
    }

    #region Implementation of IElementHandler
    public bool Accepts(IElement element)
    {
      IDeclaration declaration = element as IDeclaration;
      if (declaration == null)
      {
        return false;
      }

      return declaration.DeclaredElement.IsSpecification();
    }

    public IEnumerable<UnitTestElementDisposition> AcceptElement(IElement element, IFile file)
    {
      IDeclaration declaration = (IDeclaration) element;
      Element unitTestElement = _specificationFactory.CreateContextSpecification(declaration.DeclaredElement);

      if (unitTestElement == null)
      {
        yield break;
      }

      yield return new UnitTestElementDisposition(unitTestElement,
                                                  file.ProjectFile,
                                                  declaration.GetNameRange(),
                                                  declaration.GetDocumentRange().TextRange);
    }
    #endregion
  }
}