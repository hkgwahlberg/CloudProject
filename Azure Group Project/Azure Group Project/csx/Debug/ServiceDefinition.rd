<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Azure_Group_Project" generation="1" functional="0" release="0" Id="39f3b23d-b6d0-4b8b-b534-d31af8882a2f" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="Azure_Group_ProjectGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="GroupProjectWeb:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/LB:GroupProjectWeb:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="GroupProjectWebInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/MapGroupProjectWebInstances" />
          </maps>
        </aCS>
        <aCS name="GroupProjectWorker:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/MapGroupProjectWorker:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="GroupProjectWorkerInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/MapGroupProjectWorkerInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:GroupProjectWeb:Endpoint1">
          <toPorts>
            <inPortMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWeb/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapGroupProjectWebInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWebInstances" />
          </setting>
        </map>
        <map name="MapGroupProjectWorker:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWorker/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapGroupProjectWorkerInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWorkerInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="GroupProjectWeb" generation="1" functional="0" release="0" software="C:\Users\Belal\Source\Workspaces\CloudProject\Azure Group Project\Azure Group Project\csx\Debug\roles\GroupProjectWeb" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;GroupProjectWeb&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;GroupProjectWeb&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;GroupProjectWorker&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWebInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWebUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWebFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="GroupProjectWorker" generation="1" functional="0" release="0" software="C:\Users\Belal\Source\Workspaces\CloudProject\Azure Group Project\Azure Group Project\csx\Debug\roles\GroupProjectWorker" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;GroupProjectWorker&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;GroupProjectWeb&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;GroupProjectWorker&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWorkerInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWorkerUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWorkerFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="GroupProjectWebUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="GroupProjectWorkerUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="GroupProjectWebFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="GroupProjectWorkerFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="GroupProjectWebInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="GroupProjectWorkerInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="6231d0f3-585f-4a79-986b-96681e77455c" ref="Microsoft.RedDog.Contract\ServiceContract\Azure_Group_ProjectContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="da949d7d-b19e-4ab0-b14b-0e7d359cf1d3" ref="Microsoft.RedDog.Contract\Interface\GroupProjectWeb:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Azure_Group_Project/Azure_Group_ProjectGroup/GroupProjectWeb:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>