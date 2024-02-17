using DG.Tweening;
using StateMachine;
using UnityEngine;

public class QuestEnableState : State
{
    private readonly QuestUI m_questUI;
    private readonly QuestUIAnimationConfiguration m_questUIAnimationConfiguration;

    private Quest quest;

    public bool IsReadyToLeave;

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
    }

    public QuestEnableState(QuestUI questUI, QuestUIAnimationConfiguration questUIAnimationConfiguration)
    {
        m_questUI = questUI;
        m_questUIAnimationConfiguration = questUIAnimationConfiguration;
    }

    public override void OnStateEnter()
    {
        quest.QuestStart();
        m_questUI.QuestPanel.gameObject.SetActive(true);
        m_questUI.Description.text = quest.GetMessage();
        m_questUI.Image.sprite = quest.GetImage();
        m_questUI.Progress.text = quest.GetDescriptionProgress();
        m_questUI.Slider.fillAmount = quest.GetCurrentProgress();
        EnableAnimation();
    }

    public override void OnStateExit()
    {
        IsReadyToLeave = false;
    }

    private void EnableAnimation()
    {
        m_questUI.Image.transform.DOShakeScale(0.3f, 0.2f);
        m_questUI.QuestPanel.transform.localScale = m_questUIAnimationConfiguration.StartScale;
        m_questUI.QuestPanel.transform.position = m_questUIAnimationConfiguration.StartPosition.position;

        var sequence = DOTween.Sequence();
        sequence.Append(DOVirtual.DelayedCall(0.6f, () => { }));
        sequence.Append(
            m_questUI.QuestPanel.transform.DOMove(m_questUIAnimationConfiguration.EndPosition.position, 0.6f));
        sequence.Join(m_questUI.QuestPanel.transform.DOScale(Vector3.one, 0.6f));
        sequence.OnComplete(() => { IsReadyToLeave = true; });
    }
}