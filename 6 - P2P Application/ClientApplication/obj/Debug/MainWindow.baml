   M S B A M L   `   `   `  ����L  HClientApplication, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ClientApplicationClientApplication  5   	   "    ClientApplication.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ClientApplication     5   	     � Title$  Client Application��5   	   $	��300��6$   $	��600��61   .��5	      � .+�5
   
   � $
��Left=�6   $	��258��6*   $��0,1,0,0q�67   $	��Top=�6H   $	��160��6`   .+�5      �� $��Start Server��6   $
��Left=�6+   $��	10,10,0,0q�6F   $	��Top=�6Y   $��24(�6q   �� 5      $
��Left=�6   $��	10,57,0,0q�63   $
��Wrap=�6F   $��Client Name��6Z   $	��Top=�6m   �� 5      -   5         ��Name$ ClientNameBox��5      $
��Left=�6-   $��23��6H   $��	10,78,0,0q�6T    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��120��6�   �� 5      $
��Left=�6   $��
10,101,0,0q�63   $
��Wrap=�6G   $��Port Number��6[   $	��Top=�6n   �� 5      -   5        $ PortNumberBox��5      $
��Left=�6-   $��23��6H   $��
10,122,0,0q�6T   $
 Wrap=�6h   $	��Top=�6|   $	��120��6�   �� 5      -   5         ��Name$ StartServerButton��5      $��Start��60   $
��Left=�6@   $��
10,150,0,0q�6[   $	��Top=�6o   $��75��6�   6�   5      � 5   
   $
��Left=�6   $	��269��6*   $��	160,0,0,0q�67   $	��Top=�6J   $	��432��6b   .+�5      �� $��
Jobs Board��6   $
��Left=�6)   $��	10,10,0,0q�6D   $	��Top=�6W   $��24(�6o   �� 5      -   5         ��Name$ AvailableJobs��5      $
��Left=�6/   $��
141,22,0,0q�6J   $
��Wrap=�6^   $��Available Jobs:��6r   $	��Top=�6�   $��9(�6�   �� 5      -   5        $ CompletedJobs��5      $
��Left=�6/   $��
141,38,0,0q�6J   $
��Wrap=�6^   $��Completed Jobs:��6r   $	��Top=�6�   $��9(�6�   �� 5      $
��Left=�6   $��	10,57,0,0q�63   $
��Wrap=�6F   $
��Code��6Z   $	��Top=�6f   �� 5      -   5        $ 
JobTextBox��5      $
��Left=�6*   $��67��6E   $��10,78,-52,0q�6Q   $
 Wrap=�6f   $	��Top=�6z   $	��374��6�    �� AcceptsReturn .  �� 
AcceptsTab . 6�   �� 5      $
��Left=�6   $��
10,150,0,0q�63   $
��Wrap=�6G   $��Function Name��6[   $	��Top=�6p   �� 5      -   5        $ FunctionNameBox��5      $
��Left=�6/   $��23��6J   $��
10,171,0,0q�6V   $
 Wrap=�6j   $	��Top=�6~   $	��120��6�   �� 5      $
��Left=�6   $��137,150,0,0q�63   $
��Wrap=�6H   $��Var A��6\   $	��Top=�6i   �� 5      -   5        $ VarABox��5      $
��Left=�6'   $��23��6B   $��137,171,0,0q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��120��6�   �� 5      $
��Left=�6   $��264,150,0,0q�63   $
��Wrap=�6H   $��Var B��6\   $	��Top=�6i   �� 5      -	   5        $ VarBBox��5      $
��Left=�6'   $��23��6B   $��264,171,-52,0q�6N   $
 Wrap=�6e   $	��Top=�6y   $	��120��6�   �� 5       -
   5        $ PostJobButton��5       $��Post Job��6,   $
��Left=�6?   $��
10,199,0,0q�6Z   $	��Top=�6n   $��75��6�   6�   5!      5#      5$      