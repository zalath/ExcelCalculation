   M S B A M L   `   `   `  ����  SheetGenerator    SheetGenerator.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
MainWindow��5      $	��450��6#   $	��260��60    � WindowStyle$
 None=�6<    � WindowStartupLocation$ CenterScreen=�6O    � 
ResizeMode$ NoResize=�6t    � 	Resources 5       �� Source�� 5	   
   $ /Mainresource.xaml?�6   %    .��5      � .+�5   
   �� -   5        $��CloseButton��5         ClosebtnTemplate#��[  6$    � ToolTip$ 关闭��6N   $��Center=�6[   $��Bottom=�6x   $��0,0,0,10q�6�   �� 5   
   -   5        $��
BackButton��5      {�.   J� Opacity$ 0Q�65     LittlebuttonTemplate#��[ 6A   $��30��6o   $��30��6z   $��0��6�   
�������$ 返回��6�   $��Center=�6�   $��Bottom=�6�   $��
110,0,0,10q�6   �� 5   
   $��15,20,20,15q�6   .+�5      �� -   5        $��stCVS��5       �� Left$ 0��6"   $ 1Q�62   .+�5      �� -   5        $��calculateBtn��5   f   $��计算��6   $ 30��6&   
	 �� Top$	 60��67   
�������$��70��6Z     NextbuttonTemplate#��[ 6z   $��70��6�   6�   �� 5      -   5        $��editEquateBtn��5   i   $��编辑算式��6   $ 30��6(   $		 160��69   
�������$��70��6]   #��[ 6~   $��70��6�   6�   �� 5      -   5        $��AddUpBtn��5   i   $��累加运算��6   $ 30��6(   $		 260��69   
�������$��70��6]   #��[ 6y   $��70��6�   6�   �� 5      -   5        $��AlterDateBtn��5   i   $��修改日期��6   $	 130��6(   $	 60��6:   
�������$��70��6]   #��[ 6}   $��70��6�   6�   5      �� 5      -	   5        $��calGetDateCVS��5      $	 400��6*   $ 0Q�6<   .+�5      �� -
   5        $��
MonthTitle��5      $ 25��6*   $	 90��6;   $��选择月份：��6K   
�������6[   �� 5      -   5        $��YearCB��5      $ 30��6)   $		 140��6:   $��90��6K   �� 5      -   5        $��MonthCB��5      
 �� Text$
 1��6*   $	 130��63   $		 140��6E   $��70��6V   .@�5      �� $��1��6   �� 5      $��2��6   �� 5      $��3��6   �� 5      $��4��6   �� 5      $��5��6   �� 5       $��6��6   �� 5!      $��7��6   �� 5"      $��8��6   �� 5#      $��9��6   �� 5$      $��10��6   �� 5%      $��11��6   �� 5&      $��12��6   5'      �� 5(      -   5        $��CalCommitBtn��5(   �   $��确定��6   $��10(�6&   $��40��64   $��40��6?   $ 90��6K   $		 200��6\   
�������#��[ 6�   6�   �� 5)      -   5        $��MonthErrorLB��5)      $ 70��6,   $		 260��6=   $��月份格式不正确！��6N   
�������$ 0Q�6t   5*      �� 5,      -   5        $��calculateCVS��5,      $	 400��6*   $ 0Q�6<   .+�5-      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 6q   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6q   $ 10��6   $	 10��60   $	��335��6@   $	��200��6M   ��. $
��Auto=�6�   $
��Auto=�6�    5.      ��%Y   (��       5/   %   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   10Q�6g   50      .��51      ��  �� Orientation$ 
Horizontal=�6!   .+�52      �� -   5         ��Name$ BankListPart��52   1   $	��160��6%   53      �� 54      -   5        $ BankResultPart��54   0   $��40��6%   55      56      57      �� 58      -   5        $��ShowErrorBtn��58   ;   $	 205��6   $		 160��6*   $��错误��6O   
������� J� RenderTransformOrigin$ 0.5,0.5&�6o   z�59      V� 5:      .~�5;      ��  �� Angle$	 -90Q�6.   5<      5>      1&clr-namespace:System;assembly=mscorlibSystem 5?   r   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6r   $	 230��6   $	 10��61   $	��335��6A   $	��200��6N   ��. $
��Auto=�6�   $
��Auto=�6�    5@      ��%�   (��       5A   %   *,*SystemParameters.VerticalScrollBarWidthKey+)(��      5B   %   */-SystemParameters.HorizontalScrollBarHeightKey+)X�5A      10Q�6g   X�5B      10Q�6j   5C      .��5D      �� -   5         ��Name$ ErrorTxt��5D      $�� ��60   
���������68   Z� ��6a   Z� 5E      5F      �� 5I      -   5        $��EditListCVS��5I      $	 400��6)   $ 0Q�6;   .+�5J      �� -   5        $��DeleteEquateButton��5J      $��删除��63   
�������$	 5��6S   $	 183��6b   $��30��6t   $��25��6     RecbuttonTemplate#��[ 6�   $��Center=�6�   $��Bottom=�6�   $��0,0,0,10q�6  �� 5K      -   5        $��OrderEquateButton��5K      $��排序��62   $$ 影响计算时的执行顺序��6?   
�������$	 5��6g   $ 10��6v   $��30��6�   $��25��6�   #��[ 6�   $��Center=�6�   $��Bottom=�6�   $��0,0,0,10q�6!  �� 5L      -   5        $��OrderErrorLB��5L      $��错误��6,   $ 70��69   $		 320��6J   
���  ��$y�	Collapsed=�6l   6�   1&clr-namespace:System;assembly=mscorlibSystem 5M   �   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6�   -   5        $��EquateListSV��5M      $ 10��63   $	 40��6D   $	��300��6T   $	��215��6a   ��. $
��Auto=�6�   $
��Auto=�6�    5N      ��%Y   (��       5O   %   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   10Q�6g   5P      .��5Q      �� $ 
Horizontal=�6!   .+�5R      �� -   5        $ EquateOrderListPart��5R   0   $��35��6%   {�.  $y�	Collapsed=�6_   5S      �� 5T      -   5        $ EquateListPart��5T   1   $	��170��6%   5U      �� 5V      -   5        $ EquateEditListPart��5V   0   $��35��6%   5W      �� 5X      -   5        $ EquateDeleteListPart��5X   0   $��35��6%   {�.  $y�	Collapsed=�6`   5Y      5Z      5[      �� 5\      -   5        $��AddEquateButton��5\      $��添加��60   
�������$��12(�6P   $		 345��6^   $	 100��6o   #��[ 6�   $��30��6�   $��30��6�   $��Center=�6�   $��Bottom=�6�   $��0,0,0,10q�6  5]      �� 5_      -   5        $��EditDetailCVS��5_      $	 400��6+   $ 0Q�6=   .+�5`      �� -   5        $��hideequateNameLB��5`      $�� ��60   $y�Hidden=�6;   6Q   �� 5a      -    5        $��equateWarnLB��5a      $	 110��6,   $	 10��6>   
���  ��$��17(�6_   $��WarningText��6m   $ 0Q�6�   6�   �� 5b      -!   5        $��	NewNameLB��5b      $��算式名称：��6)   
�������$��14(�6L   $ 10��6Z   $		 -10��6k   6~   �� 5c      -"   5        $��equateNameTB��5c      $ 90��6.   $	��200��6?   $		 -10��6K   $��算式的名字��6\   6k   1&clr-namespace:System;assembly=mscorlibSystem 5d   q   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6q   $ 10��6   $	 50��60   $	��260��6@   $	��340��6M   ��. $
��Auto=�6�   $
��Auto=�6�    5e      ��%Y   (��       5f   %   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   10Q�6g   5g      .��5h      �� $ 
Horizontal=�6!   .+�5i      �� -#   5        $��EquatePartList��5i   %   $	��260��6;   6I   �� 5j      -$   5        $��EquatePartEditList��5j   %   $��60��6?   6L   5k      5l      �� 5m      -%   5        $��CommitChangeBtn��5m      $	 100��60   $		 320��6B   $��30��6S   $��30��6_   
�������#��[ 6}   $��确认��6�   6�   �� 5n      -&   5        $��CancelChangeBtn��5n      $	 210��60   $		 320��6B   $��30��6T   $��30��6`   
�������#��[ 6~   $��取消��6�   6�   5o      �� 5q      -'   5        $��EditDetail_ChosePartCVS��5q      $	 400��64   $ 0Q�6F   .+�5r      �� -(   5        $��PartName��5r      $ 40��6(   $	 10��69   
�������$��表名或列名��6\   6n   1&clr-namespace:System;assembly=mscorlibSystem 5s   q   �� 6   6sys&clr-namespace:System;assembly=mscorlib     6q   $ 20��6   $	 50��60   $	��300��6@   $	��310��6M   ��. $
��Auto=�6�   $
��Auto=�6�    5t      ��%Y   (��       5u   %   *,*SystemParameters.VerticalScrollBarWidthKey+)X�6   10Q�6g   5v      .��5w      �� $ 
Horizontal=�6!   .+�5x      �� -)   5        $��PartNameList��5x   %   $	��300��69   6G   5y      5z      5{      �� 5}      -*   5        $��EditDetail_ChoseSymbolCVS��5}      $	 400��66   $ 0Q�6H   .+�5~      �� $ 40��6   $	 10��6)   
�������$��选择符号：��6L   6^   �� 5      -+   5        $��+��5      $ 80��6&   $		 170��67   $��35��6H   $��35��6S   
�������#��[ 6r   $��20(�6�   6�   �� 5�      -,   5        $��-��5�      $	 155��6%   $		 170��67   $��35��6H   $��35��6S   
�������#��[ 6r   $��20(�6�   6�   �� 5�      --   5        $��=��5�      $	 230��6%   $		 170��67   $��35��6H   $��35��6S   
�������#��[ 6r   $��20(�6�   6�   5�      �� 5�      -.   5        $��EditDetail_ChoseDayCVS��5�      $	 400��63   $ 0Q�6E   .+�5�      �� $ 40��6   $	 10��6)   
�������$��是否累加：��6L   6^   �� 5�      -/   5        $��前日��5�      $ 80��6'   $		 170��68   $��35��6I   $��35��6T   
�������#��[ 6s   $��13(�6�   6�   �� 5�      -0   5        $��当日��5�      $	 155��6&   $		 170��68   $��35��6I   $��35��6T   
�������#��[ 6s   $��13(�6�   6�   �� 5�      -1   5        $��后日��5�      $	 230��6&   $		 170��68   $��35��6I   $��35��6T   
�������#��[ 6s   $��13(�6�   6�   5�      �� 5�      -2   5        $��EditDetail_ChoseAddupCVS��5�      $	 400��66   $ 0Q�6H   .+�5�      �� $ 40��6   $	 10��6)   
�������$��是否累加：��6L   6^   �� 5�      -3   5        $��每日��5�      $	 100��6'   $		 170��69   $��35��6J   $��35��6U   
�������#��[ 6t   $��13(�6�   6�   �� 5�      -4   5        $��累加��5�      $	 210��6&   $		 170��68   $��35��6I   $��35��6T   
�������#��[ 6s   $��13(�6�   6�   5�      �� 5�      -5   5        $��DeleteEquateCVS��5�      $	 400��6,   $ 0Q�6>   .+�5�      �� -6   5        $��DeleteEquateNameLB��5�      $ 40��62   $		 120��6C   
�������$��	是否：��6g   6w   �� 5�      -7   5        $��确定��5�      $ 50��6'   $		 210��68   $��40��6I   $��40��6T   
�������#��[ 6s   $��13(�6�   6�   �� 5�      -8   5        $��取消��5�      $	 150��6&   $		 210��68   $��40��6I   $��40��6T   
�������#��[ 6s   $��13(�6�   6�   5�      �� 5�      -9   5        $��ResultMsgCVS��5�      $��40��6)   $	��300��65   $		 150��6A   $ 0Q�6R   $	 400��6^   .+�5�      �� -:   5        $��ResultMsgLB��5�      $ 60��6+   $��是否成功��6<   
�������$��23(�6^   6n   5�      �� 5�      -;   5        $��AddupCVS��5�      $	 400��6%   $ 0Q�67   .+�5�      �� -<   5        $��AddUpProcessBankName_LB��5�      $ 0��67   $		 100��6G   
�������$
��....��6k   6|   �� 5�      -=   5        $��AddUpProcessTableName_LB��5�      $ 0��68   $		 130��6H   
�������$
��....��6l   6}   �� 5�      ->   5        $��AddUpProcessColumnName_LB��5�      $ 0��69   $		 160��6I   
�������$
��....��6m   6~   �� 5�      -?   5        $��AddUpProcessType_LB��5�      $ 0��63   $		 190��6C   
�������$
��....��6g   6x   5�      �� 5�      -@   5        $��AlterDateCVS��5�      $	 400��6)   $ 0Q�6;   .+�5�      �� -A   5        $��	MonthTitl��5�      $ 20��6)   $	 90��6:   $��数据月份：��6J   
�������6Z   �� 5�      -B   5        $��
DataYearCB��5�      $ 30��6-   $		 120��6>   $��90��6O   �� 5�      -C   5        $��DataMonthCB��5�      $
 1��6.   $	 130��67   $		 120��6I   $��70��6Z   .@�5�      �� $��1��6   �� 5�      $��2��6   �� 5�      $��3��6   �� 5�      $��4��6   �� 5�      $��5��6   �� 5�      $��6��6   �� 5�      $��7��6   �� 5�      $��8��6   �� 5�      $��9��6   �� 5�      $��10��6   �� 5�      $��11��6   �� 5�      $��12��6   5�      �� 5�      -D   5        $��MonthTil��5�      $ 20��6(   $		 160��69   $��报送日期：��6J   
�������6Z   (  "System.Windows.Controls.DatePicker  5�      -E   5        $��DataReportDate��5�      $ 30��63   $		 185��6D   $	��175��6U   6c   �� 5�      -F   5        $��Alter_commit_btn��5�   �   $��确定��6   $��10(�6&   $��40��64   $��40��6?   $ 90��6K   $		 260��6\   
�������#��[ 6�   6�   �� 5�      -G   5        $��
MonthErrLB��5�      $ 70��6*   $		 230��6;   $��月份格式不正确！��6L   
�������$ 0Q�6r   5�      5�      5�      ��5�      �� 5�   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5�      �  � Color$ 	#FF313131��6    � Offset$ 0Q�6-   � 5�      $ 	#FF4B4747��6   $ 1Q�6-   5�      5�      