
namespace StateFunding.Factors.Views
{
    public interface IFactorView//<T> where T : Factor
    {
        string getSideMenuText();

        void draw(View Vw, ViewWindow Window, Review review);
    }
}
