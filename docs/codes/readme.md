# Codes

FSMB uses codes to uniquely identify objects in the system such as medical schools, states or licensing entities. While codes are unique within their context they are not static. FSMB is constantly adding new codes. FSMB is committed to not reusing existing codes but may add new codes over time.

Each code is generally associated with a description. The description is a user friendly name for the code. Unlike a code, descriptions are not unique. Descriptions will change over time and should **NEVER** be used to identify an object. A description can change without the corresponding code changing. Clients that need unique identifiers should always use the code.

## Handling Codes in Client Code

**Clients should not rely on a fixed list of codes in any API.** 

Clients should be prepared to handle new codes when retrieving responses. If a client needs to match FSMB codes to their back end system then they will need to map from the provided FSMB code to their own unique identifier. Codes can be added without warning and therefore should be handled programmatically when possible.

Clients should make no assumptions about the format of codes. Codes are defined as strings in the JSON output. They may consist of letters, digits and symbols. Clients should treat FSMB codes simply as unique identifiers with no fixed format. FSMB codes are not case sensitive and generally are upper cased.

## Example Codes

The following is a **subset** of codes that may be returned from an API. These examples are provided as a starting point for clients to better understand FSMB codes. The examples do not include all possible codes of a single type and should be used only as a reference.

- [Accreditation Entities](accreditation.md)
- [Address Types](address-types.md)
- [Countries](countries.md)
- [Degrees](degrees.md)
- [Phone Types](phone-types.md)
- [Practitioner Types](practitioner-types.md)
- [State or Provinces](state-provinces.md)
- [Training Status](training-status.md)
