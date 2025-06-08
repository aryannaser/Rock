# MyProject - Custom Group Viewer for Rock RMS

This plugin provides a simple yet effective feature for viewing Small Group information within a Rock RMS installation. It was created to fulfill the requirements of Task 1.

It consists of two main blocks that work together: a list view to see all active small groups and a detail view to inspect the properties of a single group.

## Features

*   **Group List Block (`GroupList.ascx`)**: Displays a grid of all currently active groups of the 'Small Group' type. The grid shows the group's name and description.
*   **Group Detail Block (`GroupDetail.ascx`)**: Shows key information for a single, selected group, including its name, description, capacity, creation date, and last modified date.
*   **Click-Through Navigation**: The list block is configured to link directly to the detail block, allowing users to click a group in the list to see its details.

## How It Works

This plugin is placed in the `RockWeb/Plugins` directory. Upon application startup, Rock RMS automatically discovers the custom block files (`.ascx`) and registers them in the `BlockType` database table based on the C# attributes in the code-behind files. This makes them available to be added to any page through the administrative UI without any further configuration.

## Setup & Installation

To use this plugin in a local Rock RMS development environment:

1.  **Place Files:** Ensure the `MyProject` folder (containing the `Groups` subfolder) is located inside the `RockWeb/Plugins/` directory of your Rock RMS project.
2.  **Build Solution:** Open the main `Rock.sln` solution in Visual Studio and perform a **Build > Rebuild Solution**. This will compile the block's C# code.
3.  **Run Application:** Start the application by pressing F5. The new blocks will now be registered and available.

## Usage Guide

After building the solution, follow these steps within your running Rock RMS instance to see the feature in action:

1.  **Create Pages:** As an administrator, create two new pages:
    *   A page named `Group List`.
    *   A page named `Group Detail`.

2.  **Add Blocks:**
    *   Navigate to your `Group List` page, enter Block Edit mode, and add the **Group List** block (it will be under the **My Project > Groups** category).
    *   Navigate to your `Group Detail` page and add the **Group Detail** block.

3.  **Configure the Link:**
    *   Go back to the `Group List` page and enter Block Edit mode.
    *   Open the block properties (the gear icon) for the `Group List` block.
    *   In the **Detail Page** setting, select the `Group Detail` page you created.
    *   Save the settings.

4.  **Create Test Data:**
    *   The `Group List` block specifically looks for groups of type "Small Group". Ensure this Group Type exists (**Admin Tools > General Settings > Group Types**).
    *   Go to the **Group Viewer**, select the "Small Group" type, and create a new test group. Add your admin user as a member.

5.  **Test:** Navigate to your `Group List` page. You will see your test group listed. Click on it to be taken to the `Group Detail` page, which will display its information.