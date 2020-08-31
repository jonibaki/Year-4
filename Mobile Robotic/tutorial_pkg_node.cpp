#include "ros/ros.h"
#include "geometry_msgs/Twist.h"
#include "sensor_msgs/LaserScan.h"
#include "nav_msgs/Odometry.h"
#include <iostream>
#include <fstream>
using namespace std;


std::ofstream odomfile;
struct EulerAngles{
	double roll, pitch, yaw;
};

struct Quaternion{
	double w, x, y, z;
};
EulerAngles ToEulerAngles(Quaternion q){
	EulerAngles angles;

// roll (x-axis rotation)
	double sinr_cosp = +2.0 * (q.w * q.x + q.y * q.z);
	double cosr_cosp = +1.0 - 2.0 * (q.x * q.x + q.y * q.y);
	angles.roll = atan2(sinr_cosp, cosr_cosp);

// pitch (y-axis rotation)
	double sinp = +2.0 * (q.w * q.y - q.z * q.x);
	if (fabs(sinp) >= 1)
		angles.pitch = copysign(M_PI / 2, sinp); //use 90 degrees if out of range
	else
		angles.pitch = asin(sinp);

// yaw (z-axis rotation)
	double siny_cosp = +2.0 * (q.w * q.z + q.x * q.y);
	double cosy_cosp = +1.0 - 2.0 * (q.y * q.y + q.z * q.z);
	angles.yaw = atan2(siny_cosp, cosy_cosp);

	return angles;
}

class Stopper {
public:
// Tunable parameters
	constexpr const static double FORWARD_SPEED_LOW = 0.1;
	constexpr const static double FORWARD_SPEED_HIGH = 0.2;
	constexpr const static double FORWARD_SPEED_SHIGH = 0.4;
	constexpr const static double FORWARD_SPEED_STOP = 0;

	constexpr const static double TURN_LEFT_SPEED_HIGH = 1.0;
	constexpr const static double TURN_LEFT_SPEED_LOW = 0.3;

	constexpr const static double TURN_RIGHT_SPEED_HIGH = -0.4;
	constexpr const static double TURN_RIGHT_SPEED_LOW = -0.3;
	constexpr const static double TURN_RIGHT_SPEED_MIDDLE = -0.6;

	//varibles to allocate the robot's laser value
	float leftRange, rightRange, frontRange, rearRange;
	float posX, posY, orienX, orienY, orienZ, orienW, velX;
	int stage=1;
	double gSpeedRead=FORWARD_SPEED_HIGH;

	Stopper();
	void scanCallback(const sensor_msgs::LaserScan::ConstPtr& scan);
	void odomCallback(const nav_msgs::Odometry::ConstPtr& odomMsg);
	void startMoving();
	void moveForward(double forwardSpeed);
	void moveStop();
	void moveRightForward(double forwardSpeed, double turn_right_speed);
	void moveRight(double turn_right_speed = TURN_RIGHT_SPEED_HIGH);
private:
	ros::NodeHandle	node;
	ros::Publisher commandPub;	// Publisher to the robot's velocity command topic
 	ros::Subscriber laserSub;
	ros::Subscriber odomSub;
};

Stopper::Stopper(){
	//Advertise a new publisher for the simulated robot's velocity command topic at 10Hz
	commandPub = node.advertise<geometry_msgs::Twist>("cmd_vel", 10);
	laserSub = node.subscribe("scan",1,&Stopper::scanCallback, this);
	odomSub= node.subscribe("odom", 20,&Stopper::odomCallback, this);

}

//send a velocity command
void Stopper::moveForward(double forwardSpeed){
	geometry_msgs::Twist msg; // The default constructor will set all commands to 0
	msg.linear.x = forwardSpeed; //Drive forward at a given speed. The robot points up the x-axis.
	commandPub.publish(msg);
}

void Stopper::moveStop(){
	geometry_msgs::Twist msg;
	msg.linear.x = FORWARD_SPEED_STOP;
	commandPub.publish(msg);
}
void Stopper::moveRightForward(double forwardSpeed, double turn_right_speed){
	geometry_msgs::Twist msg;
	gSpeedRead = forwardSpeed;
	msg.linear.x = forwardSpeed;
	msg.angular.z = turn_right_speed;
	commandPub.publish(msg);
}
void Stopper::moveRight(double turn_right_speed){
	geometry_msgs::Twist msg;
	msg.angular.z = turn_right_speed;
	commandPub.publish(msg);
}

void Stopper::startMoving(){
	ros::Rate rate(20);   //Define rate for repeatable operations.
	ROS_INFO("Start moving");

	// keep spinning loop until user presses Ctrl+C
	while (ros::ok()){ //Check if ROS is working. E.g. if ROS master is stopped or there was sent signal to stop the //system, ros::ok() will return false.
		//moveForward(FORWARD_SPEED_HIGH);
		ROS_INFO_STREAM("Robot speed: " << gSpeedRead);

		//open text file to write the odometry position of the robot
		if(!odomfile){
			ROS_INFO_STREAM("File not open");
		}
		else{
			odomfile<<posX<<" ";
			odomfile<<posY<<" ";
			//odomfile<<orienX<<" ";
			//odomfile<<orienY<<" ";
			//odomfile<<orienZ<<" ";
			//odomfile<<orienW<<" ";
			odomfile<<velX<<" ";
			odomfile<<"\n";
		}

		ros::spinOnce(); // Allow ROS to process incoming messages
		rate.sleep();   // Wait until defined time passes.
	}
}
void Stopper::scanCallback(const sensor_msgs::LaserScan::ConstPtr& scan){
	frontRange = scan->ranges[0]; //get the front range from laser scanner
	rightRange = scan->ranges[539];
	leftRange = scan->ranges[180];
	rearRange = scan->ranges[359];

	ROS_INFO_STREAM("fr:"<<frontRange);
	ROS_INFO_STREAM("rr:"<<rightRange);
	ROS_INFO_STREAM("lr:"<<leftRange);
	ROS_INFO_STREAM("rearr:"<<rearRange);

	/*if(frontRange<1.5){
		moveRightForward(FORWARD_SPEED_LOW,TURN_RIGHT_SPEED_HIGH);
	}
	else{
		moveForward(FORWARD_SPEED_HIGH);
	}*/

	//SWITCH CASE TO MOVE THE ROBOT ABOUT
	switch(stage){
		case 1:
			moveForward(FORWARD_SPEED_HIGH);
			if(frontRange<1.3){
				stage=2;

			}else{

			}
			break;
		case 2:
			if(frontRange<1.3){
				moveRightForward(FORWARD_SPEED_LOW,TURN_RIGHT_SPEED_HIGH);
			}else{
				stage=3;
			}
			break;
		case 3:
			if(frontRange<1.3){
				moveRightForward(FORWARD_SPEED_LOW,TURN_RIGHT_SPEED_HIGH);
			}else{
				stage=4;
			}
		case 4:
			moveStop(); //reached goal
			break;
	}

}
void Stopper::odomCallback(const nav_msgs::Odometry::ConstPtr& odomMsg){
	posX = odomMsg->pose.pose.position.x;
	posY = odomMsg->pose.pose.position.y;

	orienX =odomMsg->pose.pose.orientation.x;
	orienY =odomMsg->pose.pose.orientation.y;
	orienZ =odomMsg->pose.pose.orientation.z;
	orienW =odomMsg->pose.pose.orientation.w;

	velX= odomMsg->twist.twist.linear.x;


	//Then do what you want with the position data

}

int main(int argc, char **argv) {
	ros::init(argc, argv, "stopper");   // Initiate new ROS node named "stopper"
	odomfile.open("/ufs/servb02/users/mj16123/record.txt"); //open the text file

	Stopper stopper; 	// Create new stopper object
	stopper.startMoving();     // Start the movement
	odomfile.close();	//close the text file

	return 0;
}
