export class Helpers {
    static convertToQueryString(obj): any {
        const parts = [];
        for (const prop in obj) {
            if (obj.hasOwnProperty(prop)) {
                const value = obj[prop];

                if (value != null && value !== undefined) {
                    parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value));
                }
            }
        }
        return parts.join('&');
    }
}
