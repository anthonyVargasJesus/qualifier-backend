using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class VersionEntity : BaseEntity
    {
        public int versionId { get; set; }
        public decimal number { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int confidentialityLevelId { get; set; }
        public int documentationId { get; set; }
        public DateTime date { get; set; }
        public bool isCurrent { get; set; }
        public string fileName { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public int? versionReferenceId { get; set; }
        public ConfidentialityLevelEntity confidentialityLevel { get; set; }
        public DocumentationEntity documentation { get; set; }

        [NotMapped]
        public List<SectionEntity> sections { get; set; }

        public void setSectionsWithChildren(List<SectionEntity> allSections)
        {
            const int FIRST_LEVEL = 1;
            const int SECOND_LEVEL = 2;
            const int THIRD_LEVEL = 3;

            sections = allSections.Where(x => x.level == FIRST_LEVEL).OrderBy(x => x.numeration).ToList();

            foreach (var section in sections)
                if (hasChildren(allSections, section.sectionId, SECOND_LEVEL))
                {
                    section.children = getChildren(allSections, section.sectionId, SECOND_LEVEL).OrderBy(x => x.numeration).ToList();

                    foreach (var item in section.children)
                        if (hasChildren(allSections, item.sectionId, THIRD_LEVEL))
                            item.children = getChildren(allSections, item.sectionId, THIRD_LEVEL).OrderBy(x => x.numeration).ToList();
                }
        }

        private bool hasChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            return allSections.Count(x => x.parentId == idSection && x.level == level) > 0;
        }

        private List<SectionEntity> getChildren(List<SectionEntity> allSections, int idSection, int level)
        {
            var entities = allSections.Where(x => x.parentId == idSection && x.level == level).ToList();
            return entities;
        }

    }
}

