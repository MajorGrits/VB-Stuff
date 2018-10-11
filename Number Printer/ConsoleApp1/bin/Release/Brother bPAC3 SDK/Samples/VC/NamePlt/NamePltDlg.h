/************************************************************************
			b-PAC 3.0 Component Sample (NamePlt)

		(C)Copyright Brother Industries, Ltsd. 2009
************************************************************************/

// NamePltDlg.h : header file
//

#if !defined(AFX_NAMEPLTDLG_H__2202B9A6_5E56_11D5_AB31_00A024CE441E__INCLUDED_)
#define AFX_NAMEPLTDLG_H__2202B9A6_5E56_11D5_AB31_00A024CE441E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CNamePltDlg dialog

class CNamePltDlg : public CDialog
{
// Construction
public:
	CNamePltDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CNamePltDlg)
	enum { IDD = IDD_NAMEPLT_DIALOG };
	CComboBox	m_cbTemplate;
	CString	m_strName;
	CString	m_strPosition;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CNamePltDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CNamePltDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnBtnPrint();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

private:
	static const CString PRINTER_NAME;
	static const CString TEMPLATE_PATH;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_NAMEPLTDLG_H__2202B9A6_5E56_11D5_AB31_00A024CE441E__INCLUDED_)
