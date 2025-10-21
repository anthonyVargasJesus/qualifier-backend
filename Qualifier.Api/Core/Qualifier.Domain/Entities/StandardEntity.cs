using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class StandardEntity : BaseEntity
    {
        public int standardId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int parentId { get; set; }
        public int companyId { get; set; }
        public StandardEntity standard { get; set; }
        public ICollection<EvaluationEntity> evaluations { get; set; }
        public ICollection<StandardEntity> standards { get; set; }
        public ICollection<UserEntity> users { get; set; }

        [NotMapped]
        public List<RequirementEntity> requirements { get; set; }

        [NotMapped]
        public List<RequirementEntity> parentRequirements { get; set; }


        [NotMapped]
        public List<ControlGroupEntity> controlsGroups { get; set; }

        public void setRequirementsWithChildren(List<RequirementEntity> allRequirements)
        {
            const int FIRST_LEVEL = 1;
            const int SECOND_LEVEL = 2;
            const int THIRD_LEVEL = 3;
            const int FOURTH_LEVEL = 4;

            requirements = allRequirements.Where(x => x.level == FIRST_LEVEL).OrderBy(x => x.numeration).ToList();

            foreach (var requirement in requirements)
                if (hasChildren(allRequirements, requirement.requirementId, SECOND_LEVEL))
                {
                    requirement.children = getChildren(allRequirements, requirement.requirementId, SECOND_LEVEL).OrderBy(x => x.numeration).ToList();

                    foreach (var item in requirement.children)
                        if (hasChildren(allRequirements, item.requirementId, THIRD_LEVEL))
                        {
                            item.children = getChildren(allRequirements, item.requirementId, THIRD_LEVEL).OrderBy(x => x.numeration).ToList();
                            foreach (var item2 in item.children)
                            {
                                if (hasChildren(allRequirements, item2.requirementId, FOURTH_LEVEL))
                                {
                                    item2.children = getChildren(allRequirements, item2.requirementId, FOURTH_LEVEL).OrderBy(x => x.numeration).ToList();
                                }
                            }
                        }
                            
                }
            setNumeration(requirements);
        }

        public string getNumerationToShow(int requirementId)
        {
            string numerationToShow = "";
            setNumeration(requirements);
            foreach (RequirementEntity item in requirements)
            {
                if (item.requirementId == requirementId)
                    numerationToShow = item.numerationToShow;

                if (item.children != null)
                    foreach (var child1 in item.children)
                    {
                        if (child1.requirementId == requirementId)
                            numerationToShow = child1.numerationToShow;

                        if (child1.children != null)
                            foreach (var child2 in child1.children)
                                if (child2.requirementId == requirementId)
                                    numerationToShow = child2.numerationToShow;

                    }
            }
            return numerationToShow;
        }


        private bool hasChildren(List<RequirementEntity> allRequirements, int idRequirement, int level)
        {
            return allRequirements.Count(x => x.parentId == idRequirement && x.level == level) > 0;
        }

        private List<RequirementEntity> getChildren(List<RequirementEntity> allRequirements, int idRequirement, int level)
        {
            var entities = allRequirements.Where(x => x.parentId == idRequirement && x.level == level).ToList();
            return entities;
        }

        //private void setNumeration(List<RequirementEntity> localRequirements)
        //{
        //    foreach (RequirementEntity item in localRequirements)
        //    {
        //        if (item.letter != null)
        //            item.numerationToShow = item.letter != "" ? item.letter : item.numeration.ToString();

        //        if (item.children != null)
        //            foreach (var child1 in item.children)
        //            {
        //                if (child1.letter != null)
        //                    child1.numerationToShow = child1.letter != "" ? child1.letter : (item.numeration + "." + child1.numeration);

        //                if (child1.children != null)
        //                    foreach (var child2 in child1.children)
        //                    {
        //                        if (child2.letter != null)
        //                            child2.numerationToShow = child2.letter != "" ? child2.letter : (item.numeration + "." + child1.numeration + "." + child2.numeration);

        //                        if (child2.children != null)
        //                        {
        //                            foreach (var child3 in child2.children)
        //                            {
        //                                if (child3.letter != null)
        //                                    child3.numerationToShow = child3.letter != "" ? child3.letter : (item.numeration + "." + child1.numeration + "." + child2.numeration + "." + child3.numeration);
        //                            }
        //                        }
        //                    }
        //            }
        //    }
        //}

        private void setNumeration(List<RequirementEntity> requirements, string parentNumeration = "", string parentBreadcrumb = "")
        {
            foreach (var item in requirements)
            {
                // Determinar numeración del item actual
                if (!string.IsNullOrEmpty(item.letter))
                {
                    item.numerationToShow = item.letter;
                }
                else
                {
                    item.numerationToShow = string.IsNullOrEmpty(parentNumeration)
                        ? item.numeration.ToString()
                        : parentNumeration + "." + item.numeration;
                }

                // Determinar breadcrumb del item actual
                item.breadcrumbToShow = string.IsNullOrEmpty(parentBreadcrumb)
                    ? item.numerationToShow
                    : parentBreadcrumb + " > " + item.numerationToShow;

                // Si tiene hijos, llamar recursivamente pasando numeration y breadcrumb acumulados
                if (item.children != null && item.children.Any())
                {
                    setNumeration(item.children, item.numerationToShow, item.breadcrumbToShow);
                }
            }
        }


        public void setControlsWithChildren(List<ControlGroupEntity> allControlGroups, List<ControlEntity> allControls)
        {
            controlsGroups = allControlGroups;
            foreach (var group in allControlGroups)
            {
                group.controls = allControls.Where(e => e.controlGroupId == group.controlGroupId).ToList();
                foreach (var control in group.controls)
                    control.numerationToShow = group.number + "." + control.number;
            }
        }
        public string getControlNumerationToShow(int controlId)
        {
            string numerationToShow = "";
            foreach (var group in controlsGroups)
            {
                foreach (var control in group.controls)
                    if (control.controlId == controlId)
                        numerationToShow = control.numerationToShow;
            }
            return numerationToShow;
        }



        public void setEvaluationsToRequirements(List<RequirementEvaluationEntity> evaluations)
        {
            foreach (RequirementEntity item in requirements)
            {
                if (item.children == null)
                {
                    item.requirementEvaluations = evaluations.Where(x => x.requirementId == item.requirementId).ToList();
                    if (item.requirementEvaluations != null)
                        if (item.requirementEvaluations.Count == 0)
                            item.requirementEvaluations.Add(getDefaultRequierementeEvaluation(item));

                    if (item.requirementEvaluations != null)
                        setNumerationToEvaluations(item.requirementEvaluations.ToList(), item.numerationToShow, item.breadcrumbToShow);
                }
                if (item.children != null)
                    foreach (var child1 in item.children)
                    {
                        if (child1.children == null)
                        {
                            child1.requirementEvaluations = evaluations.Where(x => x.requirementId == child1.requirementId).ToList();

                            if (child1.requirementEvaluations != null)
                                if (child1.requirementEvaluations.Count == 0)
                                    child1.requirementEvaluations.Add(getDefaultRequierementeEvaluation(child1));

                            if (child1.requirementEvaluations != null)
                                setNumerationToEvaluations(child1.requirementEvaluations.ToList(), child1.numerationToShow, child1.breadcrumbToShow);
                        }

                        if (child1.children != null)
                            foreach (var child2 in child1.children)
                            {
                                if (child2.children == null)
                                {
                                    child2.requirementEvaluations = evaluations.Where(x => x.requirementId == child2.requirementId).ToList();
                                    if (child2.requirementEvaluations != null)
                                        if (child2.requirementEvaluations.Count == 0)
                                            child2.requirementEvaluations.Add(getDefaultRequierementeEvaluation(child2));
                                    if (child2.requirementEvaluations != null)
                                        setNumerationToEvaluations(child2.requirementEvaluations.ToList(), child2.numerationToShow, child2.breadcrumbToShow);
                                }

                                if (child2.children != null)
                                {

                                    foreach (var child3 in child2.children)
                                    {
                                        if (child3.children == null)
                                        {
                                            child3.requirementEvaluations = evaluations.Where(x => x.requirementId == child3.requirementId).ToList();
                                            if (child3.requirementEvaluations != null)
                                                if (child3.requirementEvaluations.Count == 0)
                                                    child3.requirementEvaluations.Add(getDefaultRequierementeEvaluation(child3));
                                            if (child3.requirementEvaluations != null)
                                                setNumerationToEvaluations(child3.requirementEvaluations.ToList(), child3.numerationToShow, child3.breadcrumbToShow);
                                        }



                                    }

                                }
                            }
                                
                    }
            }
        }

        private RequirementEvaluationEntity getDefaultRequierementeEvaluation(RequirementEntity requirement)
        {
            return new RequirementEvaluationEntity
            {
                requirementEvaluationId = 0,
                requirementId = requirement.requirementId,
                state = "Pendiente",
                percentage = 0,
                auditorStatus = 0,
                requirement = new RequirementEntity
                {
                    requirementId = requirement.requirementId,
                    name = requirement.name,
                },
            };
        }

        private void setNumerationToEvaluations(List<RequirementEvaluationEntity> requirementEvaluations, string numerationToShow, string? breadcrumbToShow)
        {
            foreach (var item in requirementEvaluations)
                if (item.requirement != null)
                {
                    item.requirement.numerationToShow = numerationToShow;
                    item.requirement.breadcrumbToShow = breadcrumbToShow;
                }
                   
        }


        public void setTotalValuesToRequirements(List<RequirementEvaluationEntity> evaluations, List<MaturityLevelEntity> generalMaturityLevels)
        {

            foreach (MaturityLevelEntity maturityLevel in generalMaturityLevels)
                maturityLevel.value = 0;


            foreach (RequirementEntity requirement in requirements)
            {
                List<MaturityLevelEntity> maturityLevelsxRequirement = new List<MaturityLevelEntity>();
                foreach (var item in generalMaturityLevels)
                {
                    MaturityLevelEntity maturityLevelxRequirement = new MaturityLevelEntity();
                    maturityLevelxRequirement.maturityLevelId = item.maturityLevelId;
                    maturityLevelxRequirement.name = item.name;
                    maturityLevelxRequirement.value = item.value;
                    maturityLevelxRequirement.factor = item.factor;
                    maturityLevelsxRequirement.Add(maturityLevelxRequirement);
                }

                foreach (var maturityLevel in maturityLevelsxRequirement)
                {
                    decimal totalValue = 0;
                    if (requirement.children == null)
                        foreach (RequirementEvaluationEntity requirementEvaluation in evaluations)
                            if (requirementEvaluation.maturityLevelId == maturityLevel.maturityLevelId
                                && requirementEvaluation.requirementId == requirement.requirementId)
                                totalValue = totalValue + requirementEvaluation.value;

                    if (requirement.children != null)
                        foreach (var child1 in requirement.children)
                        {
                            if (child1.children == null)
                                foreach (RequirementEvaluationEntity requirementEvaluation in evaluations)
                                    if (requirementEvaluation.maturityLevelId == maturityLevel.maturityLevelId
                                        && requirementEvaluation.requirementId == child1.requirementId)
                                        totalValue = totalValue + requirementEvaluation.value;


                            if (child1.children != null)
                                foreach (var child2 in child1.children)
                                    if (child2.children == null)
                                        foreach (RequirementEvaluationEntity requirementEvaluation in evaluations)
                                            if (requirementEvaluation.maturityLevelId == maturityLevel.maturityLevelId
                                                && requirementEvaluation.requirementId == child2.requirementId)
                                                totalValue = totalValue + requirementEvaluation.value;
                        }
                    maturityLevel.value = totalValue;
                }

                requirement.maturityLevels = maturityLevelsxRequirement;
            }

        }


        public void setTotalValueInMaturityLevels(List<MaturityLevelEntity> maturityLevels)
        {
            foreach (MaturityLevelEntity item in maturityLevels)
            {
                decimal totalValue = 0;

                foreach (var requirement in requirements)
                    foreach (MaturityLevelEntity maturityLevel in requirement.maturityLevels)
                        if (item.maturityLevelId == maturityLevel.maturityLevelId)
                            totalValue = totalValue + maturityLevel.value;
                item.value = totalValue;
            }
        }

        public void setPercentInMaturityLevels(List<MaturityLevelEntity> maturityLevels, decimal totalValuexEvaluation)
        {
            foreach (var item in maturityLevels)
                if (totalValuexEvaluation > 0)
                    item.percent = Math.Round((item.value * 100) / totalValuexEvaluation, 2);
        }

        public decimal getTotalValuexEvaluation()
        {
            decimal totalValuexEvaluation = 0;
            foreach (var requirement in requirements)
            {
                decimal totalValue = 0;
                foreach (MaturityLevelEntity maturityLevel in requirement.maturityLevels)
                    totalValue += maturityLevel.value;
                requirement.value = totalValue;
                totalValuexEvaluation = totalValuexEvaluation + totalValue;
            }
            return totalValuexEvaluation;
        }

        public void setIndicators(List<IndicatorEntity> indicators)
        {
            const decimal FACTOR = 5;
            foreach (var requirement in requirements)
            {
                decimal numerator = 0;
                decimal denominator = 0;
                foreach (MaturityLevelEntity item in requirement.maturityLevels)
                {
                    if (item.factor != null)
                        numerator = numerator + item.value * item.factor.Value;

                    denominator = denominator + item.value;
                }

                decimal result = 0;
                if (denominator > 0)
                    result = (numerator / denominator) * FACTOR;

                requirement.setIndicator(indicators, result);
            }
        }

        public List<PieDashboardRequirement> getPieChartDashboard(List<MaturityLevelEntity> maturityLevels)
        {
            List<PieDashboardRequirement> pieDashboardRequirements = new List<PieDashboardRequirement>();
            foreach (var item in maturityLevels)
            {
                PieDashboardRequirement pieDashboardRequirementDto = new PieDashboardRequirement();
                pieDashboardRequirementDto.name = item.name;
                pieDashboardRequirementDto.value = item.value;
                pieDashboardRequirements.Add(pieDashboardRequirementDto);
            }
            return pieDashboardRequirements;
        }


        public List<BartVerticalDashboardRequirement> getBarChartDashboard()
        {
            List<BartVerticalDashboardRequirement> barVerticalDashboardRequirements = new List<BartVerticalDashboardRequirement>();
            foreach (var requirement in requirements)
            {
                BartVerticalDashboardRequirement barVerticalDashboardRequirement = new BartVerticalDashboardRequirement();
                barVerticalDashboardRequirement.name = requirement.name;


                List<DashboardRequirementSerie> series = new List<DashboardRequirementSerie>();
                foreach (var item in requirement.maturityLevels)
                {
                    DashboardRequirementSerie serie = new DashboardRequirementSerie();
                    serie.name = item.name;
                    serie.value = item.value;
                    series.Add(serie);
                }
                barVerticalDashboardRequirement.series = series;
                barVerticalDashboardRequirements.Add(barVerticalDashboardRequirement);
            }
            return barVerticalDashboardRequirements;
        }

        public List<string> getColors(List<MaturityLevelEntity> maturityLevels)
        {
            List<string> colors = new List<string>();
            foreach (var item in maturityLevels)
                colors.Add((item.color == null) ? "" : item.color);
            return colors;
        }

        public void setTotalValuesToControls(List<ControlEvaluationEntity> evaluations, List<MaturityLevelEntity> generalMaturityLevels)
        {

            foreach (MaturityLevelEntity maturityLevel in generalMaturityLevels)
                maturityLevel.value = 0;

            foreach (ControlGroupEntity controlGroup in controlsGroups)
            {
                List<MaturityLevelEntity> maturityLevelsxControl = new List<MaturityLevelEntity>();
                foreach (var item in generalMaturityLevels)
                {
                    MaturityLevelEntity maturityLevelxControl = new MaturityLevelEntity();
                    maturityLevelxControl.maturityLevelId = item.maturityLevelId;
                    maturityLevelxControl.name = item.name;
                    maturityLevelxControl.value = item.value;
                    maturityLevelxControl.factor = item.factor;
                    maturityLevelsxControl.Add(maturityLevelxControl);
                }

                foreach (var maturityLevel in maturityLevelsxControl)
                {
                    decimal totalValue = 0;

                    foreach (ControlEvaluationEntity controlEvaluation in evaluations)
                        if (controlEvaluation.maturityLevelId == maturityLevel.maturityLevelId
                            && controlEvaluation.controlGroupId == controlGroup.controlGroupId)
                            totalValue = totalValue + controlEvaluation.value;

                    maturityLevel.value = totalValue;
                }

                controlGroup.maturityLevels = maturityLevelsxControl;
            }
        }

        public void setTotalValueControlsInMaturityLevels(List<MaturityLevelEntity> maturityLevels)
        {
            foreach (MaturityLevelEntity item in maturityLevels)
            {
                decimal totalValue = 0;

                foreach (var controlGroup in controlsGroups)
                    foreach (MaturityLevelEntity maturityLevel in controlGroup.maturityLevels)
                        if (item.maturityLevelId == maturityLevel.maturityLevelId)
                            totalValue = totalValue + maturityLevel.value;
                item.value = totalValue;
            }
        }

        public decimal getTotalControlValuexEvaluation()
        {
            decimal totalValuexEvaluation = 0;
            foreach (var controlGroup in controlsGroups)
            {
                decimal totalValue = 0;
                foreach (MaturityLevelEntity maturityLevel in controlGroup.maturityLevels)
                    totalValue += maturityLevel.value;
                controlGroup.value = totalValue;
                totalValuexEvaluation = totalValuexEvaluation + totalValue;
            }
            return totalValuexEvaluation;
        }

        public void setControlIndicators(List<IndicatorEntity> indicators)
        {
            const decimal FACTOR = 5;
            foreach (var controlGroup in controlsGroups)
            {
                decimal numerator = 0;
                decimal denominator = 0;
                foreach (MaturityLevelEntity item in controlGroup.maturityLevels)
                {
                    if (item.factor != null)
                        numerator = numerator + item.value * item.factor.Value;

                    denominator = denominator + item.value;
                }

                decimal result = 0;
                if (denominator > 0)
                    result = (numerator / denominator) * FACTOR;

                controlGroup.setIndicator(indicators, result);
            }
        }

        public void setPercentControlsInMaturityLevels(List<MaturityLevelEntity> maturityLevels, decimal totalValuexEvaluation)
        {
            foreach (var item in maturityLevels)
                if (totalValuexEvaluation > 0)
                    item.percent = Math.Round((item.value * 100) / totalValuexEvaluation, 2);
        }


        public List<PieControlDashboardControlGroup> getPieControlChartDashboard(List<MaturityLevelEntity> maturityLevels)
        {
            List<PieControlDashboardControlGroup> pieDashboardControlGroups = new List<PieControlDashboardControlGroup>();
            foreach (var item in maturityLevels)
            {
                PieControlDashboardControlGroup pieDashboardControlGroupDto = new PieControlDashboardControlGroup();
                pieDashboardControlGroupDto.name = item.name;
                pieDashboardControlGroupDto.value = item.value;
                pieDashboardControlGroups.Add(pieDashboardControlGroupDto);
            }
            return pieDashboardControlGroups;
        }


        public List<BartVerticalControlDashboard> getControlBarChartDashboard()
        {
            List<BartVerticalControlDashboard> barVerticalDashboardRequirements = new List<BartVerticalControlDashboard>();

            foreach (var controlGroup in controlsGroups)
            {
                BartVerticalControlDashboard barVerticalDashboardRequirement = new BartVerticalControlDashboard();
                barVerticalDashboardRequirement.name = controlGroup.name;

                List<DashboardControlGroupSerie> series = new List<DashboardControlGroupSerie>();
                foreach (var item in controlGroup.maturityLevels)
                {
                    DashboardControlGroupSerie serie = new DashboardControlGroupSerie();
                    serie.name = item.name;
                    serie.value = item.value;
                    series.Add(serie);
                }
                barVerticalDashboardRequirement.series = series;
                barVerticalDashboardRequirements.Add(barVerticalDashboardRequirement);
            }
            return barVerticalDashboardRequirements;
        }


        public void setReferenceDocumentationToEvaluations(List<RequirementEvaluationEntity> evaluations, List<ReferenceDocumentationEntity> referenceDocumentations)
        {
            foreach (RequirementEvaluationEntity item in evaluations)
                item.referenceDocumentations = referenceDocumentations.Where(x => x.requirementEvaluationId == item.requirementEvaluationId).ToList();
        }



    }
}

