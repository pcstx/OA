import java.util.Date;

/**
 * 活动实体类
 * 在前后台之间传输的json数据格式需要包含一下属性，如果是数组，则为activity格式的json数组。
 * 
 */

public class Activity {
	
	private String id;
	private String title;
	private String contents;
	private Date startTime;
	private Date endTime;
	private String place;
	
	public String getId() {
		return id;
	}
	public void setId(String id) {
		this.id = id;
	}
	public String getTitle() {
		return title;
	}
	public void setTitle(String title) {
		this.title = title;
	}
	public String getContents() {
		return contents;
	}
	public void setContents(String contents) {
		this.contents = contents;
	}
	public Date getStartTime() {
		return startTime;
	}
	public void setStartTime(Date startTime) {
		this.startTime = startTime;
	}
	public Date getEndTime() {
		return endTime;
	}
	public void setEndTime(Date endTime) {
		this.endTime = endTime;
	}
	public String getPlace() {
		return place;
	}
	public void setPlace(String place) {
		this.place = place;
	}
}
