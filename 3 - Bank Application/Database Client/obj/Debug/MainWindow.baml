   M S B A M L   `   `   `  ����J  FDatabase Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Database_ClientDatabase_Client  5   	        Database_Client.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Database_Client     5   	     � Title$  Bank Application��5   	   $	��360��6"   $	��300��6/   .��5	      � .+�5
   
   �� -   5         ��Name$ 	UserImage��5
      $
��Left=�6$   $��80��6?   $��
135,10,0,0q�6K   $	��Top=�6_   $��80��6w   �� 5   
   -   5         ��Name$ FirstNameTextBox��5       �� 
IsReadOnly . $
��Left=�6>   $��23��6Y   $��	10,10,0,0q�6e    �� TextWrapping$
 Wrap=�6x   $��
First Name��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ LastNameTextBox��5       . $
��Left=�6=   $��23��6X   $��	10,38,0,0q�6d   $
 Wrap=�6w   $��	Last Name��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ AccountNoTextBox��5       . $
��Left=�6>   $��23��6Y   $��	10,66,0,0q�6e   $
 Wrap=�6x   $��Account Number��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ 
PINTextBox��5       . $
��Left=�68   $��23��6S   $��	10,94,0,0q�6_   $
 Wrap=�6r   $	��PIN��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ BalanceTextBox��5       . $
��Left=�6<   $��23��6W   $��
10,122,0,0q�6c   $
 Wrap=�6w   $��Balance��6�   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ IndexTextBox��5      $
��Left=�6(   $��23��6C   $��
10,226,0,0q�6O   $
 Wrap=�6c   $��Index��6w   $	��Top=�6�   $	��120��6�   �� 5   
   -   5         ��Name$ IndexButton��5      $	��Go!��6&   $
��Left=�64   $��135,226,0,0q�6O   $	��Top=�6d   $��75��6|   $��23��6�   6�   �� 5   
   -	   5         ��Name$ TotalItemsLabel��5      $��Total Items: ��6)   $
��Left=�6A   $��
10,195,0,0q�6\   $	��Top=�6p   �� 5   
   -
   5        $ SearchTextBox��5      $
��Left=�6)   $��23��6D   $��
10,263,0,0q�6P   $
 Wrap=�6d   $��Search��6x   $	��Top=�6�   $	��120��6�   �� 5   
   -   5        $ SearchButton��5      $��Search!��6'   $
��Left=�69   $��135,263,0,0q�6T   $	��Top=�6i   $��75��6�   $��23��6�   6�   � 5   
   -   5         �Name$ SearchProgressBar��5      $
��Left=�61   $��10��6L   $��
10,291,0,0q�6X   $	��Top=�6l   $	��100��6�   5      5      