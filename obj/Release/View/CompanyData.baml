   M S B A M L   `   `   `  ���� N  JAnnualReportCrawler, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AnnualReportCrawler.ViewAnnualReportCrawler.View  M+clr-namespace:AnnualReportCrawler.ViewModelAnnualReportCrawler.ViewModel  M+clr-namespace:AnnualReportCrawler.ConverterAnnualReportCrawler.Converter  *    $AnnualReportCrawler.View.CompanyData   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     8local&clr-namespace:AnnualReportCrawler.View     A	viewModel+clr-namespace:AnnualReportCrawler.ViewModel     A	converter+clr-namespace:AnnualReportCrawler.Converter     e aTelerik.Windows.Controls, Version=2015.2.728.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2015.2.728.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2015.2.728.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2015.2.728.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2015.2.728.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p
 lTelerik.Windows.Controls.Navigation, Version=2015.2.728.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
   � DataContext  8   2AnnualReportCrawler.ViewModel.CompanyDataViewModel  .���  � RowDefinitions �� $
��Auto ��� $��* ��� $��* ��� .+���  �� Orientation$ 
Horizontal=�.+��� $	��300��$��50��$��10q����� *	Companies+ �� SelectedItem �� *SelectedCompany+���� .R��� ���� *Name+�� $	��150��$��25��$��Go������ *GetCompanyDetails+*  $Telerik.Windows.Controls.RadGridView  $��1��;  5Telerik.Windows.Controls.GridView.GridViewDataControl   AutoGenerateColumns .   J� 	AllowDrop .     ShowGroupPanel .     CanUserSortColumns .     IsFilteringAllowed .  $��10q�*  $Telerik.Windows.Controls.DataControl	   ItemsSource	 �� *CompanyIncomeInfo+
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 1  +Telerik.Windows.Controls.GridViewDataColumn  6  0Telerik.Windows.Controls.GridViewBoundColumnBase   Header$ Filing��-  'Telerik.Windows.Controls.GridViewColumn   IsGroupable .     DataMemberBinding �� *	RowHeader+  $ Format�� .   �� *
ValueCell1+   	IsVisible �� *Info1Visibility+$
 TwoWay=�$ PropertyChanged=�  $
 Name�� .   �� *
ValueCell2+ �� *Info2Visibility+$
 TwoWay=�$ PropertyChanged=�  $ 
FilingDate�� .   �� *
ValueCell3+ �� *Info3Visibility+$
 TwoWay=�$ PropertyChanged=�  $ 
FilingDate�� .   �� *
ValueCell4+ �� *Info4Visibility+$
 TwoWay=�$ PropertyChanged=�  $ 
FilingDate�� .   �� *
ValueCell5+ �� *Info5Visibility+$
 TwoWay=�$ PropertyChanged=�  $��2�� .   .   .   .   .  $��10q�	 �� *CompanySegmentInfo+$
 TwoWay=�$ PropertyChanged=�   $ Company-Info�� .   �� *	RowHeader+  $ Period�� .   �� *
ValueCell1+ �� *Cell1Visibility+$
 TwoWay=�$ PropertyChanged=�  $ Period�� .   �� *
ValueCell2+ �� *Cell2Visibility+$
 TwoWay=�$ PropertyChanged=�  $ Period�� .   �� *
ValueCell3+ �� *Cell3Visibility+$
 TwoWay=�$ PropertyChanged=�  $ Period�� .   �� *
ValueCell4+ �� *Cell4Visibility+$
 TwoWay=�$ PropertyChanged=�  $ Period�� .   �� *
ValueCell5+ �� *Cell5Visibility+$
 TwoWay=�$ PropertyChanged=�