
using UnityEngine;

namespace StateFunding.ViewComponents
{
    public class ViewSummaryRow : RelativeViewComponent
    {
        private ViewLabel mainLabel;
        private ViewLabel poLabel;
        private ViewLabel scLabel;

        public ViewSummaryRow(string summaryText, int modPO, int modSc)
        {
            mainLabel = new ViewLabel(summaryText);
            mainLabel.setRelativeTo(this);
            mainLabel.setLeft(10);
            mainLabel.setTop(0);
            mainLabel.setColor(Color.white);
            mainLabel.setHeight(20);
            mainLabel.setWidth(300);

            if(modPO != 0)
            {
                poLabel = new ViewLabel("PO: " + modPO);
                poLabel.setRelativeTo(this);
                poLabel.setLeft(310);
                poLabel.setTop(0);
                poLabel.setColor(Color.white);
                poLabel.setHeight(20);
                poLabel.setWidth(100);
            }

            if (modSc != 0)
            {
                scLabel = new ViewLabel("SC: " + modSc);
                scLabel.setRelativeTo(this);
                scLabel.setLeft(410);
                scLabel.setTop(0);
                scLabel.setColor(Color.white);
                scLabel.setHeight(20);
                scLabel.setWidth(100);
            }
        }

        public override void paint()
        {
            mainLabel.paint();

            if (poLabel != null)
            {
                poLabel.paint();
            }
            if (scLabel != null)
            {
                scLabel.paint();
            }
        }
    }
}
