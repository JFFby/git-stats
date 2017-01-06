# git stats: Common provides common statistic of git repository

## Usage

Specify path for your repository

	cd [path]

for example you can run app from VS and specify

	cd ../

result:

![](http://content.screencast.com/users/JFFby/folders/Snagit/media/f0bb672b-9479-41c0-829a-9486c6b5073b/11.20.2016-13.45.png)

You also can see statistic between two commits

	between [commit1] [commit2]

for example:

	between a72fdb2 29697aa

result:

![](http://content.screencast.com/users/JFFby/folders/Snagit/media/91eabc15-be18-4cb6-911e-810cee36e259/11.27.2016-14.24.png)

You can merge user's statistic with 'merge' command

	merge [mainAuthorId] [duplicatedAuthorIds]

for example:
	
	merge 1 2

result:

![](http://content.screencast.com/users/JFFby/folders/Snagit/media/e3333bb2-c3b1-45c2-b9b9-47712ca3f748/11.27.2016-15.46.png)

You can specify time frame

	datedif [from date] [to date]
	
for example:

	datedif 19/11/2016 20/11/2016
	
![](http://content.screencast.com/users/JFFby/folders/Snagit/media/c1df3a6d-4e31-4efd-a211-339d302dc3f7/01.04.2017-22.03.png)

Also you can group statistics by weeks

	datedif [from] [to] weekly

![](http://content.screencast.com/users/JFFby/folders/Snagit/media/4b73c189-b41d-4b94-af62-77fbcf640dae/01.06.2017-18.54.png)