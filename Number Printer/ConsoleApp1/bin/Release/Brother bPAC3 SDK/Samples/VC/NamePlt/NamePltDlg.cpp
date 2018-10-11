/************************************************************************
			b-PAC 3.0 Component Sample (NamePlt)

		(C)Copyright Brother Industries, Ltsd. 2009
************************************************************************/

// NamePltDlg.cpp : implementation file
//

#include "stdafx.h"
#include "NamePlt.h"
#include "NamePltDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CNamePltDlg dialog
const CString CNamePltDlg::TEMPLATE_PATH = _T("C:\\Program Files\\Brother bPAC3 SDK\\Templates\\");

CNamePltDlg::CNamePltDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CNamePltDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CNamePltDlg)
	m_strName = _T("");
	m_strPosition = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CNamePltDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CNamePltDlg)
	DDX_Control(pDX, IDC_COMBO_TEMPLATE, m_cbTemplate);
	DDX_Text(pDX, IDC_EDIT_NAME, m_strName);
	DDX_Text(pDX, IDC_EDIT_POSITION, m_strPosition);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CNamePltDlg, CDialog)
	//{{AFX_MSG_MAP(CNamePltDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BTN_PRINT, OnBtnPrint)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CNamePltDlg message handlers

BOOL CNamePltDlg::OnInitDialog()
{
	// Set Paremeter
	m_strName.LoadString(IDS_NAME_STRING);
	m_strPosition.LoadString(IDS_POSITION_STRING);

	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// Set Combobox
	m_cbTemplate.SetCurSel(0);

	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CNamePltDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CNamePltDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

// Print Label
void CNamePltDlg::OnBtnPrint() 
{
	UpdateData(TRUE);

	try {
		IDocumentPtr pObjDoc(__uuidof(Document));
		CString strPath = TEMPLATE_PATH;
		if (m_cbTemplate.GetCurSel() <= 0)
			strPath += _T("NamePlate1.lbx");
		else
			strPath += _T("NamePlate2.lbx");
		if (pObjDoc->Open(strPath.AllocSysString()))
		{
			pObjDoc->GetObject(_bstr_t(_T("objCompany")))->PutText(m_strPosition.AllocSysString());
			pObjDoc->GetObject(_bstr_t(_T("objName")))->PutText(m_strName.AllocSysString());
			
			// pObjDoc->SetMediaById(pObjDoc->Printer->GetMediaId(), true);
			pObjDoc->StartPrint(_bstr_t(_T("")), bpac::bpoDefault);
			pObjDoc->PrintOut(1, bpac::bpoDefault);
			pObjDoc->EndPrint();
			pObjDoc->Close();
		}
	}
	catch (_com_error e) {
	   AfxMessageBox(e.ErrorMessage());
	}
}
