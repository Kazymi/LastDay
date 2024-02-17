using StateMachine;

public class QuestActiveState : State
{
    private readonly QuestUI m_questUI;

    private Quest quest;

    public bool IsReadyToLeave => quest.IsQuestCompleted();

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
    }

    public QuestActiveState(QuestUI questUI)
    {
        m_questUI = questUI;
    }

    public override void Tick()
    {
        m_questUI.Progress.text = quest.GetDescriptionProgress();
        m_questUI.Slider.fillAmount = quest.GetCurrentProgress();
    }
}